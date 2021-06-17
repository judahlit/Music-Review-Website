using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
using Music_Review_Application_DB_Managers.Interfaces;
using Music_Review_Application_Models;

namespace Music_Review_Application_DB_Managers
{
    public class AlbumDbManager : IAlbumDbManager
    {
        #region Constants and Fields

        private const string QueryAddAlbum = "INSERT INTO album(title, dateOfRelease, img) OUTPUT INSERTED.id VALUES('{0}','{1}',CONVERT(VARBINARY(MAX),'{2}'));";
        private const string QueryAddAlbumArtist = "INSERT INTO albumArtist(albumId, artistId) VALUES({0},{1});";
        private const string QueryAddReview = "INSERT INTO AlbumReview(albumId, username, albumScore, albumReview) VALUES({0},'{1}',{2},'{3}');";

        private const string QueryGetAlbumId = "SELECT A.id FROM Album AS A INNER JOIN AlbumArtist AS AA ON A.id = AA.AlbumId WHERE A.title = '{0}' AND AA.artistId = {1};";
        private const string QueryGetAlbum = "SELECT * FROM Album WHERE id = {0};";
        private const string QueryGetAlbumArtists = "SELECT * FROM AlbumArtist WHERE albumId = {0};";
        private const string QueryGetAlbumTrackIds = "SELECT id FROM Song WHERE albumId = {0};";
        private const string QueryGetScore = "SELECT AVG(albumScore) FROM AlbumReview WHERE albumId = {0};";
        private const string QueryGetReviewId = "SELECT id FROM AlbumReview WHERE albumId = {0} AND username = '{1}';";
        private const string QueryGetReview = "SELECT * FROM AlbumReview WHERE id = {0};";
        private const string QueryGetArtistAlbums = "SELECT AA.albumId FROM AlbumArtist AS AA INNER JOIN Artist AS A ON AA.artistId = A.id WHERE A.id = {0}";
        private const string QueryGetAllAlbumIds = "SELECT id FROM Album;";

        private const string QueryUpdateReview = "UPDATE AlbumReview SET albumScore = {1}, albumReview = '{2}' WHERE id = {0};";

        private const string QueryDeleteAlbum = "DELETE FROM Album WHERE id = '{0}';";
        private const string QueryDeleteAlbumReviews = "DELETE FROM AlbumReview WHERE albumId = '{0}';";
        private const string QueryDeleteAlbumArtists = "DELETE FROM AlbumArtist WHERE albumId = '{0}';";
        private const string QueryDeleteReview = "DELETE FROM AlbumReview WHERE id = '{0}';";

        private readonly ISqlManager _sqlManager;
        private readonly IArtistDbManager _artistDbManager;
        private readonly ISongDbManager _songDbManager;
        private readonly IImageConverter _imageConverter;

        #endregion

        public AlbumDbManager(ISqlManager sqlManager, IArtistDbManager artistDbManager, ISongDbManager songDbManager,
            IImageConverter imageConverter)
        {
            _sqlManager = sqlManager;
            _artistDbManager = artistDbManager;
            _songDbManager = songDbManager;
            _imageConverter = imageConverter;
        }

        #region Methods

        public void AddAlbum(Album album)
        {
            if (AlbumExistsInDb(album))
            {
                return;
            }

            _artistDbManager.CheckArtists(album.ArtistNames);

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

        public void AddReview(AlbumReview albumReview)
        {
            if (GetReviewId(albumReview.AlbumId, albumReview.Username) != 0) return;

            using (SqlConnection conn = new SqlConnection(SqlManager.ConnectionString))
            {
                using (SqlCommand query = new SqlCommand(string.Format(QueryAddReview, albumReview.AlbumId, _sqlManager.GetSqlString(albumReview.Username), albumReview.Score, _sqlManager.GetSqlString(albumReview.Review)), conn))
                {
                    conn.Open();
                    var reader = query.ExecuteReader();
                    reader.Close();
                }
            }
        }

        public int GetAlbumId(string title, List<string> artistNames)
        {
            List<List<int>> albumIds = new();
            List<int> artistIds = new();
            int artistNumber = 0;

            foreach (var artistName in artistNames)
            {
                artistIds.Add(_artistDbManager.GetArtistId(artistName));
            }

            if (artistIds.Count == 1 && artistIds[0] == 0)
            {
                return 0;
            }

            using (SqlConnection conn = new SqlConnection(SqlManager.ConnectionString))
            {
                foreach (var artistId in artistIds)
                {
                    using (SqlCommand query = new SqlCommand(string.Format(QueryGetAlbumId, _sqlManager.GetSqlString(title), artistId), conn))
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

            if (albumIds.Count == 0) return 0;

            foreach (var albumId in albumIds[0])
            {
                var amountOfAnAlbumId = 0;

                for (var index = 1; index < albumIds.Count; index++)
                {
                    if (albumIds.Count <= index) continue;

                    foreach (var anAlbumId in albumIds[index])
                    {
                        if (anAlbumId == albumId)
                        {
                            amountOfAnAlbumId++;
                        }
                    }
                }

                if (amountOfAnAlbumId == albumIds.Count - 1)
                {
                    return albumId;
                }
            }

            return 0;
        }

        public double GetScore(int albumId)
        {
            var score = 0.0f;

            using (SqlConnection conn = new SqlConnection(SqlManager.ConnectionString))
            {
                using (SqlCommand query = new SqlCommand(string.Format(QueryGetScore, albumId), conn))
                {
                    conn.Open();
                    var reader = query.ExecuteReader();

                    if (reader.Read())
                    {
                        if (reader.GetValue(0) != DBNull.Value)
                        {
                            score = Convert.ToSingle(reader.GetValue(0));
                        }
                    }

                    reader.Close();
                }
            }

            return score;
        }

        public int GetReviewId(int albumId, string username)
        {
            var id = 0;

            using (SqlConnection conn = new SqlConnection(SqlManager.ConnectionString))
            {
                using (SqlCommand query = new SqlCommand(string.Format(QueryGetReviewId, albumId, _sqlManager.GetSqlString(username)), conn))
                {
                    conn.Open();
                    var reader = query.ExecuteReader();

                    if (reader.Read())
                    {
                        id = reader.GetInt32(0);
                    }

                    reader.Close();
                }
            }

            return id;
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
                using (SqlCommand query = new SqlCommand(string.Format(QueryGetAlbum, id), conn))
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

                using (SqlCommand query = new SqlCommand(string.Format(QueryGetAlbumArtists, id), conn))
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

        public AlbumReview GetAlbumReview(int id)
        {
            var albumId = 0;
            var username = "";
            var score = 0.0f;
            var review = "";

            using (SqlConnection conn = new SqlConnection(SqlManager.ConnectionString))
            {
                using (SqlCommand query = new SqlCommand(string.Format(QueryGetReview, id), conn))
                {
                    conn.Open();
                    var reader = query.ExecuteReader();

                    while (reader.Read())
                    {
                        albumId = reader.GetInt32(1);
                        username = reader.GetString(2);
                        score = Convert.ToSingle(reader.GetValue(3));
                        review = reader.GetString(4);
                    }
                }
            }

            return new AlbumReview(albumId, username, score, review) { Id = id };
        }

        public List<Album> GetArtistAlbums (int artistId)
        {
            var albums = new List<Album>();

            using (SqlConnection conn = new SqlConnection(SqlManager.ConnectionString))
            {
                using (SqlCommand query = new SqlCommand(string.Format(QueryGetArtistAlbums, artistId), conn))
                {
                    conn.Open();
                    var reader = query.ExecuteReader();

                    while (reader.Read())
                    {
                        albums.Add(GetAlbum(reader.GetInt32(0)));
                    }
                }
            }

            return albums;
        }

        public List<Album> GetAlbums()
        {
            var albums = new List<Album>();

            using (SqlConnection conn = new SqlConnection(SqlManager.ConnectionString))
            {
                using (SqlCommand query = new SqlCommand(string.Format(QueryGetAllAlbumIds), conn))
                {
                    conn.Open();
                    var reader = query.ExecuteReader();

                    while (reader.Read())
                    {
                        albums.Add(GetAlbum(reader.GetInt32(0)));
                    }
                }
            }

            return albums;
        }

        public void UpdateReview(AlbumReview albumReview)
        {
            if (GetReviewId(albumReview.AlbumId, albumReview.Username) == 0) return;

            using (SqlConnection conn = new SqlConnection(SqlManager.ConnectionString))
            {
                using (SqlCommand query = new SqlCommand(string.Format(QueryUpdateReview, GetReviewId(albumReview.AlbumId, albumReview.Username), albumReview.Score, _sqlManager.GetSqlString(albumReview.Review)), conn))
                {
                    conn.Open();
                    var reader = query.ExecuteReader();
                    reader.Close();
                }
            }
        }

        public void DeleteAlbum(int id)
        {
            Album album = GetAlbum(id);

            foreach (Track track in album.Tracks)
            {
                _songDbManager.DeleteSong(track.Id);
            }

            using (SqlConnection conn = new SqlConnection(SqlManager.ConnectionString))
            {
                using (SqlCommand query = new SqlCommand(string.Format(QueryDeleteAlbumReviews, id), conn))
                {
                    conn.Open();
                    query.ExecuteNonQuery();
                }

                using (SqlCommand query = new SqlCommand(string.Format(QueryDeleteAlbumArtists, id), conn))
                {
                    query.ExecuteNonQuery();
                }

                using (SqlCommand query = new SqlCommand(string.Format(QueryDeleteAlbum, id), conn))
                {
                    query.ExecuteNonQuery();
                }
            }
        }

        public void DeleteReview(int id)
        {
            using (SqlConnection conn = new SqlConnection(SqlManager.ConnectionString))
            {
                using (SqlCommand query = new SqlCommand(string.Format(QueryDeleteReview, id), conn))
                {
                    conn.Open();
                    query.ExecuteNonQuery();
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
            if (!AlbumExistsInDb(album))
            {
                AddAlbum(album);
                List<Genre> albumGenres = album.GetAlbumGenres();

                Album album1 = GetAlbum(GetAlbumId(album.Title, album.ArtistNames));
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
