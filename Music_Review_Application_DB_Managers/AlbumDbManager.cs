using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using Music_Review_Application_Models;
using ImageConverter = Music_Review_Application_Models.ImageConverter;

namespace Music_Review_Application_DB_Managers
{
    public class AlbumDbManager
    {
        #region Constants and Fields

        private const string QueryAddAlbum = "INSERT INTO album(title, dateOfRelease, img) OUTPUT INSERTED.id VALUES('{0}','{1}',CONVERT(VARBINARY(MAX),'{2}'));";
        private const string QueryAddAlbumArtist = "INSERT INTO albumArtist(albumId, artistId) VALUES({0},{1});";
        private const string QueryGetAlbumId = "SELECT Album.id FROM Album, AlbumArtist WHERE Album.title = '{0}' AND Album.id = AlbumArtist.AlbumId AND AlbumArtist.artistId = (SELECT id FROM Artist WHERE artistName = '{1}');";
        private const string QueryGetAlbumById = "SELECT * FROM Album WHERE id = {0};";
        private const string QueryGetAlbumArtistsByAlbumId = "SELECT * FROM AlbumArtist WHERE albumId = {0};";
        private const string QueryGetAlbumTrackIds = "SELECT id FROM Song WHERE albumId = {0};";
        private const string QueryGetAllAlbums = "";
        private const string QueryGetSortedAlbums = "";
        private const string QueryUpdateScore = "";
        private const string QueryGetUserScore = "";
        private const string QueryDeleteAlbum = "DELETE FROM Album WHERE id = '{0}'";
        private const string QueryDeleteAlbumArtists = "DELETE FROM AlbumArtist WHERE albumId = '{0}'";

        private readonly SqlManager _sqlManager = new();
        private readonly ArtistDbManager _artistDbManager = new();
        private readonly SongDbManager _songDbManager = new();
        private readonly ImageConverter _imageConverter = new();

        #endregion

        #region Methods

        public void AddAlbum(Album album)
        {
            if (AlbumExistsInDb(album))
            {
                return;
            }

            CheckArtists(album);

            byte[] bytearray = _imageConverter.ImageToByteArray(album.Img);
            int albumId = 0;

            using (SqlConnection conn = new SqlConnection(SqlManager.ConnectionString))
            {
                using (SqlCommand query = new SqlCommand(string.Format(QueryAddAlbum, _sqlManager.GetSqlString(album.Title), album.DateOfRelease, bytearray), conn))
                {
                    conn.Open();
                    var reader = query.ExecuteReader();

                    if (reader.Read())
                    {
                        albumId = reader.GetInt32(0);
                    }

                    reader.Close();
                }

                foreach (string artistName in album.ArtistNames)
                {
                    using (SqlCommand query = new SqlCommand(string.Format(QueryAddAlbumArtist, albumId, _artistDbManager.GetArtistId(artistName)), conn))
                    {
                        query.ExecuteNonQuery();
                    }
                }
            }

            foreach (Track track in album.Tracks)
            {
                _songDbManager.AddTrack(track, albumId);
            }
        }

        public int GetAlbumId(string title, List<string> artistNames)
        {
            List<List<int>> albumIds = new();
            int artistNumber = 0;

            using (SqlConnection conn = new SqlConnection(SqlManager.ConnectionString))
            {
                foreach (string artistName in artistNames)
                {
                    using (SqlCommand query = new SqlCommand(string.Format(QueryGetAlbumId, _sqlManager.GetSqlString(title), _sqlManager.GetSqlString(artistName)), conn))
                    {
                        conn.Open();
                        var reader = query.ExecuteReader();

                        if (reader.HasRows)
                        {
                            albumIds.Add(new());
                        }

                        while (reader.Read())
                        {
                            albumIds[artistNumber].Add(reader.GetInt32(0));
                        }

                        reader.Close();
                        conn.Close();
                    }

                    artistNumber++;
                }
            }

            if (albumIds.Count != 0)
            {
                foreach (int albumId in albumIds[0])
                {
                    int amountOfAnAlbumId = 0;

                    for (int index = 1; index < albumIds.Count; index++)
                    {
                        if (albumIds.Count > index)
                        {
                            foreach (int anAlbumId in albumIds[index])
                            {
                                if (anAlbumId == albumId)
                                {
                                    amountOfAnAlbumId++;
                                }
                            }
                        }
                    }

                    if (amountOfAnAlbumId == albumIds.Count - 1)
                    {
                        return albumId;
                    }
                }
            }

            return 0;
        }


        public Album GetAlbum(int id)
        {
            string title = "";
            List<Track> tracks = new();
            DateTime dateOfRelease = new();
            Image img = null;
            List<string> artistNames = new();

            List<int> trackSongIds = new();

            using (SqlConnection conn = new SqlConnection(SqlManager.ConnectionString))
            {
                using (SqlCommand query = new SqlCommand(string.Format(QueryGetAlbumById, id), conn))
                {
                    conn.Open();
                    var reader = query.ExecuteReader();

                    while (reader.Read())
                    {
                        title = reader.GetString(1);
                        dateOfRelease = (DateTime)reader["dateOfRelease"];

                        if (reader["img"] != DBNull.Value)
                        {
                            img = _imageConverter.ByteArrayToImage((byte[])reader["img"]);
                        }
                    }

                    reader.Close();
                }

                using (SqlCommand query = new SqlCommand(string.Format(QueryGetAlbumArtistsByAlbumId, id), conn))
                {
                    var reader = query.ExecuteReader();

                    while (reader.Read())
                    {
                        artistNames.Add(_artistDbManager.GetArtist(reader.GetInt32(2)).ArtistName);
                    }

                    reader.Close();
                }

                using (SqlCommand query = new SqlCommand(string.Format(QueryGetAlbumTrackIds, id), conn))
                {
                    var reader = query.ExecuteReader();

                    while (reader.Read())
                    {
                        trackSongIds.Add(reader.GetInt32(0));
                    }

                    reader.Close();
                }
            }

            foreach (int trackSongId in trackSongIds)
            {
                tracks.Add(_songDbManager.GetTrack(trackSongId));
            }

            Album album = new(title, tracks, dateOfRelease, img, artistNames)
            {
                Id = id,
                Score = 0
            };

            return album;
        }

        /*

        public List<Album> GetAlbums()
        {

        }

        public List<Album> GetAlbums(string title, bool byDate, bool byScore, List<string> genres)
        {

        }

        public AlbumScore GetAlbumScore(Album album, string username)
        {

        }

        public void UpdateScore(int albumId, AlbumScore userScore)
        {

        }
        */

        public void DeleteAlbum(int id)
        {
            Album album = GetAlbum(id);

            foreach (Track track in album.Tracks)
            {
                _songDbManager.DeleteTrack(track.Id);
            }

            using (SqlConnection conn = new SqlConnection(SqlManager.ConnectionString))
            {
                using (SqlCommand query = new SqlCommand(string.Format(QueryDeleteAlbumArtists, id), conn))
                {
                    conn.Open();
                    query.ExecuteNonQuery();
                }

                using (SqlCommand query = new SqlCommand(string.Format(QueryDeleteAlbum, id), conn))
                {
                    query.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }

        private void CheckArtists(Album album)
        {
            foreach (string artistName in album.ArtistNames)
            {
                if (_artistDbManager.GetArtistId(artistName) == 0)
                {
                    Artist artist = new(artistName, null, null);
                    _artistDbManager.AddArtist(artist);
                }
            }
        }

        public bool AlbumExistsInDb(Album album)
        {
            if (GetAlbumId(album.Title, album.ArtistNames) == 0)
            {
                return false;
            }

            return true;
        }

        public bool AlbumIsAdded(Album album)
        {
            AlbumDbManager albumDbManager = new();

            if (albumDbManager.GetAlbumId(album.Title, album.ArtistNames) == 0)
            {
                albumDbManager.AddAlbum(album);
                List<Genre> albumGenres = album.GetAlbumGenres();

                Album album1 = albumDbManager.GetAlbum(albumDbManager.GetAlbumId(album.Title, album.ArtistNames));
                List<Genre> album1Genres = album1.GetAlbumGenres();

                if (album.Title == album1.Title && album.Tracks.Count == album1.Tracks.Count && album.DateOfRelease == album1.DateOfRelease && album.Img == album1.Img && album.ArtistNames.Count == album1.ArtistNames.Count && albumGenres.Count == album1Genres.Count)
                {
                    return true;
                }
            }

            return false;
        }

        #endregion
    }
}
