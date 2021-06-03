using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
using Music_Review_Application_DB_Managers.Interfaces;
using Music_Review_Application_Models;
using Music_Review_Application_Services.Interfaces;

namespace Music_Review_Application_DB_Managers
{
    public class SongDbManager : ISongDbManager
    {
        #region Constants and Fields

        private const string QueryAddSingle = "INSERT INTO Song(title, dateOfRelease, img) OUTPUT INSERTED.id VALUES('{0}','{1}',CONVERT(VARBINARY(MAX),'{2}'));";
        private const string QueryAddTrack = "INSERT INTO Song(title, dateOfRelease, trackNr, albumId) OUTPUT INSERTED.id VALUES('{0}','{1}',{2},{3});";
        private const string QueryAddSongArtist = "INSERT INTO SongArtist(songId, artistId) VALUES({0}, {1});";
        private const string QueryAddSongGenre = "INSERT INTO SongGenre(songId, genreId) VALUES({0},{1});";
        private const string QueryGetSongId = "SELECT Song.id FROM Song, songArtist WHERE Song.title = '{0}' AND Song.id = SongArtist.songId AND SongArtist.artistId = (SELECT id FROM Artist WHERE artistName = '{1}');";
        private const string QueryGetSongById = "SELECT * FROM Song WHERE id = {0};";
        private const string QueryGetSongArtistsBySongId = "SELECT * FROM SongArtist WHERE songId = {0};";
        private const string QueryGetSongGenreIdsBySongId = "SELECT * FROM SongGenre WHERE songId = {0};";

        private const string QueryGetAllSongs = "";
        private const string QueryGetSortedSongs = "";
        private const string QueryUpdateScore = "";
        private const string QueryGetUserScore = "";
        private const string QueryDeleteSong = "DELETE FROM Song WHERE id = '{0}'";
        private const string QueryDeleteSongArtist = "DELETE FROM SongArtist WHERE songId = '{0}'";
        private const string QueryDeleteSongGenre = "DELETE FROM SongGenre WHERE songId = '{0}'";

        private readonly ISqlManager _sqlManager;
        private readonly IArtistDbManager _artistDbManager;
        private readonly IGenreDbManager _genreDbManager;
        private readonly IImageConverter _imageConverter;

        #endregion

        public SongDbManager(ISqlManager sqlManager, IArtistDbManager artistDbManager, IGenreDbManager genreDbManager, IImageConverter imageConverter)
        {
            _sqlManager = sqlManager;
            _artistDbManager = artistDbManager;
            _genreDbManager = genreDbManager;
            _imageConverter = imageConverter;
        }

        #region Methods

        public void AddSingle(SingleSong single)
        {
            if (SongExistsInDb(single))
            {
                return;
            }

            _artistDbManager.CheckArtists(single.ArtistNames);
            _genreDbManager.CheckGenres(single.Genres);

            int songId = 0;
            byte[] bytearray = _imageConverter.ImageToByteArray(single.Img);

            using (SqlConnection conn = new SqlConnection(SqlManager.ConnectionString))
            {
                using (SqlCommand query = new SqlCommand(string.Format(QueryAddSingle, _sqlManager.GetSqlString(single.Title), single.DateOfRelease, bytearray), conn))
                {
                    conn.Open();
                    var reader = query.ExecuteReader();

                    if (reader.Read())
                    {
                        songId = reader.GetInt32(0);
                    }

                    reader.Close();
                }

                foreach (string artistName in single.ArtistNames)
                {
                    using (SqlCommand query = new SqlCommand(string.Format(QueryAddSongArtist, songId, _artistDbManager.GetArtistId(artistName)), conn))
                    {
                        query.ExecuteNonQuery();
                    }
                }

                foreach (Genre genre in single.Genres)
                {
                    using (SqlCommand query = new SqlCommand(string.Format(QueryAddSongGenre, songId, _genreDbManager.GetGenreId(genre.GenreName)), conn))
                    {
                        query.ExecuteNonQuery();
                    }
                }
            }
        }

        public void AddTrack(Track track, int albumId)
        {
            if (SongExistsInDb(track))
            {
                return;
            }

            _artistDbManager.CheckArtists(track.ArtistNames);
            _genreDbManager.CheckGenres(track.Genres);

            int songId = 0;

            using (SqlConnection conn = new SqlConnection(SqlManager.ConnectionString))
            {
                using (SqlCommand query = new SqlCommand(string.Format(QueryAddTrack, _sqlManager.GetSqlString(track.Title), track.DateOfRelease, track.TrackNr, albumId), conn))
                {
                    conn.Open();
                    var reader = query.ExecuteReader();

                    if (reader.Read())
                    {
                        songId = reader.GetInt32(0);
                    }

                    reader.Close();
                }

                foreach (string artistName in track.ArtistNames)
                {
                    using (SqlCommand query = new SqlCommand(string.Format(QueryAddSongArtist, songId, _artistDbManager.GetArtistId(artistName)), conn))
                    {
                        query.ExecuteNonQuery();
                    }
                }

                foreach (Genre genre in track.Genres)
                {
                    using (SqlCommand query = new SqlCommand(string.Format(QueryAddSongGenre, songId, _genreDbManager.GetGenreId(genre.GenreName)), conn))
                    {
                        query.ExecuteNonQuery();
                    }
                }
            }
        }

        public int GetSongId(string title, List<string> artistNames)
        {
            List<List<int>> songIds = new();
            int artistNumber = 0;

            using (SqlConnection conn = new SqlConnection(SqlManager.ConnectionString))
            {
                foreach (string artistName in artistNames)
                {
                    using (SqlCommand query = new SqlCommand(string.Format(QueryGetSongId, _sqlManager.GetSqlString(title), _sqlManager.GetSqlString(artistName)), conn))
                    {
                        conn.Open();
                        var reader = query.ExecuteReader();

                        if (reader.HasRows)
                        {
                            songIds.Add(new());
                        }

                        while (reader.Read())
                        {
                            songIds[artistNumber].Add(reader.GetInt32(0));
                        }

                        reader.Close();
                        conn.Close();
                    }

                    artistNumber++;
                }
            }

            if (songIds.Count != 0)
            {
                foreach (int songId in songIds[0])
                {
                    int amountOfASongId = 0;

                    for (int index = 1; index < songIds.Count; index++)
                    {
                        if (songIds.Count > index)
                        {
                            foreach (int aSongId in songIds[index])
                            {
                                if (aSongId == songId)
                                {
                                    amountOfASongId++;
                                }
                            }
                        }
                    }

                    if (amountOfASongId == songIds.Count - 1)
                    {
                        return songId;
                    }
                }
            }

            return 0;
        }

        public SingleSong GetSingle(int id)
        {
            string title = null;
            DateTime dateOfRelease = new();
            Image img = null;
            List<string> artistNames = new();
            List<Genre> genres = new();

            using (SqlConnection conn = new SqlConnection(SqlManager.ConnectionString))
            {
                using (SqlCommand query = new SqlCommand(string.Format(QueryGetSongById, id), conn))
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

                using (SqlCommand query = new SqlCommand(string.Format(QueryGetSongArtistsBySongId, id), conn))
                {
                    var reader = query.ExecuteReader();

                    while (reader.Read())
                    {
                        artistNames.Add(_artistDbManager.GetArtist(reader.GetInt32(2)).ArtistName);
                    }

                    reader.Close();
                }

                using (SqlCommand query = new SqlCommand(string.Format(QueryGetSongGenreIdsBySongId, id), conn))
                {
                    var reader = query.ExecuteReader();

                    while (reader.Read())
                    {
                        genres.Add(_genreDbManager.GetGenre(reader.GetInt32(2)));
                    }

                    reader.Close();
                }
            }

            SingleSong single = new(title, dateOfRelease, img, artistNames, genres)
            {
                Id = id,
                Score = 0
            };

            return single;
        }

        public Track GetTrack(int id)
        {
            string title = null;
            DateTime dateOfRelease = new();
            int trackNr = 0;
            int albumId = 0;
            List<string> artistNames = new();
            List<Genre> genres = new();

            using (SqlConnection conn = new SqlConnection(SqlManager.ConnectionString))
            {
                using (SqlCommand query = new SqlCommand(string.Format(QueryGetSongById, id), conn))
                {
                    conn.Open();
                    var reader = query.ExecuteReader();

                    while (reader.Read())
                    {
                        title = reader.GetString(1);
                        dateOfRelease = (DateTime)reader["dateOfRelease"];
                        trackNr = reader.GetInt32(4);
                        albumId = reader.GetInt32(5);
                    }

                    reader.Close();
                }

                using (SqlCommand query = new SqlCommand(string.Format(QueryGetSongArtistsBySongId, id), conn))
                {
                    var reader = query.ExecuteReader();

                    while (reader.Read())
                    {
                        artistNames.Add(_artistDbManager.GetArtist(reader.GetInt32(2)).ArtistName);
                    }

                    reader.Close();
                }

                using (SqlCommand query = new SqlCommand(string.Format(QueryGetSongGenreIdsBySongId, id), conn))
                {
                    var reader = query.ExecuteReader();

                    while (reader.Read())
                    {
                        genres.Add(_genreDbManager.GetGenre(reader.GetInt32(2)));
                    }

                    reader.Close();
                }
            }

            Track track = new(title, dateOfRelease, trackNr, artistNames, genres)
            {
                Id = id,
                Score = 0,
                AlbumId = albumId
            };

            return track;
        }

        public void DeleteSingle(int id)
        {
            using (SqlConnection conn = new SqlConnection(SqlManager.ConnectionString))
            {
                using (SqlCommand query = new SqlCommand(string.Format(QueryDeleteSongGenre, id), conn))
                {
                    conn.Open();
                    query.ExecuteNonQuery();
                }

                using (SqlCommand query = new SqlCommand(string.Format(QueryDeleteSongArtist, id), conn))
                {
                    query.ExecuteNonQuery();
                }

                using (SqlCommand query = new SqlCommand(string.Format(QueryDeleteSong, id), conn))
                {
                    query.ExecuteNonQuery();
                }
            }
        }

        public void DeleteTrack(int id)
        {
            using (SqlConnection conn = new SqlConnection(SqlManager.ConnectionString))
            {
                using (SqlCommand query = new SqlCommand(string.Format(QueryDeleteSongGenre, id), conn))
                {
                    conn.Open();
                    query.ExecuteNonQuery();
                }

                using (SqlCommand query = new SqlCommand(string.Format(QueryDeleteSongArtist, id), conn))
                {
                    query.ExecuteNonQuery();
                }

                using (SqlCommand query = new SqlCommand(string.Format(QueryDeleteSong, id), conn))
                {
                    query.ExecuteNonQuery();
                }
            }
        }

        public bool SongExistsInDb(Song song)
        {
            if (GetSongId(song.Title, song.ArtistNames) == 0)
            {
                return false;
            }

            return true;
        }

        public bool SingleIsAdded(SingleSong single)
        {
            if (GetSongId(single.Title, single.ArtistNames) == 0)
            {
                AddSingle(single);
                single.Id = GetSongId(single.Title, single.ArtistNames);
                SingleSong single1 = GetSingle(GetSongId(single.Title, single.ArtistNames));

                if (single.Id == single1.Id && single.Img == single1.Img) return true;
            }

            return false;
        }

        public bool TrackIsAdded(Track track, int albumId)
        {
            if (GetSongId(track.Title, track.ArtistNames) == 0)
            {
                AddTrack(track, albumId);
                track.Id = GetSongId(track.Title, track.ArtistNames);
                Track track1 = GetTrack(GetSongId(track.Title, track.ArtistNames));

                if (track.Id == track1.Id && track.AlbumId == track1.AlbumId && track.TrackNr == track1.TrackNr) return true;
            }

            return false;
        }

        #endregion
    }
}
