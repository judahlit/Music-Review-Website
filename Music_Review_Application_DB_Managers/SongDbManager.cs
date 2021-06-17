using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
using Music_Review_Application_DB_Managers.Interfaces;
using Music_Review_Application_Models;

namespace Music_Review_Application_DB_Managers
{
    public class SongDbManager : ISongDbManager
    {
        #region Constants and Fields

        private const string QueryAddSingle = "INSERT INTO Song(title, dateOfRelease, img) OUTPUT INSERTED.id VALUES('{0}','{1}',CONVERT(VARBINARY(MAX),'{2}'));";
        private const string QueryAddTrack = "INSERT INTO Song(title, dateOfRelease, trackNr, albumId) OUTPUT INSERTED.id VALUES('{0}','{1}',{2},{3});";
        private const string QueryAddSongArtist = "INSERT INTO SongArtist(songId, artistId) VALUES({0}, {1});";
        private const string QueryAddSongGenre = "INSERT INTO SongGenre(songId, genreId) VALUES({0},{1});";
        private const string QueryAddReview = "INSERT INTO SongReview(songId, username, songScore, songReview) VALUES({0},'{1}',{2},'{3}');";

        private const string QueryGetSongId = "SELECT S.id FROM Song AS S INNER JOIN songArtist AS SA ON S.id = SA.songId WHERE S.title = '{0}' AND SA.artistId = {1};";
        private const string QueryGetSong = "SELECT * FROM Song WHERE id = {0};";
        private const string QueryGetSongArtists = "SELECT * FROM SongArtist WHERE songId = {0};";
        private const string QueryGetSongGenreIds = "SELECT * FROM SongGenre WHERE songId = {0};";
        private const string QueryGetScore = "SELECT AVG(songScore) FROM SongReview WHERE songId = {0};";
        private const string QueryGetReviewId = "SELECT id FROM SongReview WHERE songId = {0} AND username = '{1}';";
        private const string QueryGetReview = "SELECT * FROM SongReview WHERE id = {0};";
        private const string QueryGetAllSongIds = "SELECT id FROM Song;";
        private const string QueryGetSongIdsWithGenre = "SELECT songId FROM SongGenre WHERE genreId = {0};";
        private const string QueryGetArtistSongs = "SELECT SA.songId FROM SongArtist AS SA INNER JOIN Artist AS A ON SA.artistId = A.id WHERE A.id = {0}";

        private const string QueryUpdateReview = "UPDATE SongReview SET songScore = {1}, songReview = '{2}' WHERE id = {0};";

        private const string QueryDeleteSongs = "DELETE FROM Song WHERE id = '{0}';";
        private const string QueryDeleteSongArtists = "DELETE FROM SongArtist WHERE songId = '{0}';";
        private const string QueryDeleteSongGenres = "DELETE FROM SongGenre WHERE songId = '{0}';";
        private const string QueryDeleteSongReviews = "DELETE FROM SongReview WHERE songId = '{0}';";
        private const string QueryDeleteReview = "DELETE FROM SongReview WHERE id = '{0}';";

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

        public void AddReview(SongReview songReview)
        {
            if (GetReviewId(songReview.SongId, songReview.Username) != 0) return;

            using (SqlConnection conn = new SqlConnection(SqlManager.ConnectionString))
            {
                using (SqlCommand query = new SqlCommand(string.Format(QueryAddReview, songReview.SongId, _sqlManager.GetSqlString(songReview.Username), songReview.Score, _sqlManager.GetSqlString(songReview.Review)), conn))
                {
                    conn.Open();
                    var reader = query.ExecuteReader();
                    reader.Close();
                }
            }
        }

        public int GetSongId(string title, List<string> artistNames)
        {
            List<List<int>> songIds = new();
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
                    using (SqlCommand query = new SqlCommand(string.Format(QueryGetSongId, _sqlManager.GetSqlString(title), artistId), conn))
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

            if (songIds.Count == 0) return 0;

            foreach (var songId in songIds[0])
            {
                var amountOfASongId = 0;

                for (var index = 1; index < songIds.Count; index++)
                {
                    if (songIds.Count <= index) continue;

                    foreach (var aSongId in songIds[index])
                    {
                        if (aSongId == songId)
                        {
                            amountOfASongId++;
                        }
                    }
                }

                if (amountOfASongId == songIds.Count - 1)
                {
                    return songId;
                }
            }

            return 0;
        }

        public double GetScore(int songId)
        {
            var score = 0.0f;

            using (SqlConnection conn = new SqlConnection(SqlManager.ConnectionString))
            {
                using (SqlCommand query = new SqlCommand(string.Format(QueryGetScore, songId), conn))
                {
                    conn.Open();
                    var reader = query.ExecuteReader();

                    if (reader.Read())
                    {
                        if (reader.GetValue(0) != DBNull.Value)
                        {
                            score = (float)reader.GetValue(0);
                        }
                    }

                    reader.Close();
                }
            }

            return score;
        }

        public int GetReviewId(int songId, string username)
        {
            var id = 0;

            using (SqlConnection conn = new SqlConnection(SqlManager.ConnectionString))
            {
                using (SqlCommand query = new SqlCommand(string.Format(QueryGetReviewId, songId, _sqlManager.GetSqlString(username)), conn))
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

        public SingleSong GetSingle(int id)
        {
            string title = null;
            DateTime dateOfRelease = new();
            Image img = null;
            List<string> artistNames = new();
            List<Genre> genres = new();

            using (SqlConnection conn = new SqlConnection(SqlManager.ConnectionString))
            {
                using (SqlCommand query = new SqlCommand(string.Format(QueryGetSong, id), conn))
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

                using (SqlCommand query = new SqlCommand(string.Format(QueryGetSongArtists, id), conn))
                {
                    var reader = query.ExecuteReader();

                    while (reader.Read())
                    {
                        artistNames.Add(_artistDbManager.GetArtist(reader.GetInt32(2)).ArtistName);
                    }

                    reader.Close();
                }

                using (SqlCommand query = new SqlCommand(string.Format(QueryGetSongGenreIds, id), conn))
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
                using (SqlCommand query = new SqlCommand(string.Format(QueryGetSong, id), conn))
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

                using (SqlCommand query = new SqlCommand(string.Format(QueryGetSongArtists, id), conn))
                {
                    var reader = query.ExecuteReader();

                    while (reader.Read())
                    {
                        artistNames.Add(_artistDbManager.GetArtist(reader.GetInt32(2)).ArtistName);
                    }

                    reader.Close();
                }

                using (SqlCommand query = new SqlCommand(string.Format(QueryGetSongGenreIds, id), conn))
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

        public Song GetSong(int id)
        {
            string title = null;
            DateTime dateOfRelease = new();
            Image img = null;
            List<string> artistNames = new();
            List<Genre> genres = new();
            int trackNr = 0;
            int albumId = 0;

            using (SqlConnection conn = new SqlConnection(SqlManager.ConnectionString))
            {
                using (SqlCommand query = new SqlCommand(string.Format(QueryGetSong, id), conn))
                {
                    conn.Open();
                    var reader = query.ExecuteReader();

                    while (reader.Read())
                    {
                        title = reader.GetString(1);
                        dateOfRelease = (DateTime)reader["dateOfRelease"];

                        if (reader["trackNr"] != DBNull.Value && reader["albumId"] != DBNull.Value)
                        {
                            trackNr = reader.GetInt32(4);
                            albumId = reader.GetInt32(5);
                        }

                        if (reader["img"] != DBNull.Value)
                        {
                            img = _imageConverter.ByteArrayToImage((byte[])reader["img"]);
                        }
                    }

                    reader.Close();
                }

                using (SqlCommand query = new SqlCommand(string.Format(QueryGetSongArtists, id), conn))
                {
                    var reader = query.ExecuteReader();

                    while (reader.Read())
                    {
                        artistNames.Add(_artistDbManager.GetArtist(reader.GetInt32(2)).ArtistName);
                    }

                    reader.Close();
                }

                using (SqlCommand query = new SqlCommand(string.Format(QueryGetSongGenreIds, id), conn))
                {
                    var reader = query.ExecuteReader();

                    while (reader.Read())
                    {
                        genres.Add(_genreDbManager.GetGenre(reader.GetInt32(2)));
                    }

                    reader.Close();
                }
            }

            if (title == null) return null;

            if (albumId == 0)
            {
                return new SingleSong(title, dateOfRelease, img, artistNames, genres)
                {
                    Id = id,
                    Score = 0
                };
            }

            return new Track(title, dateOfRelease, trackNr, artistNames, genres)
            {
                Id = id,
                Score = 0,
                AlbumId = albumId
            };
        }

        public SongReview GetSongReview(int id)
        {
            var songId = 0;
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
                        songId = reader.GetInt32(1);
                        username = reader.GetString(2);
                        score = (float)reader.GetValue(3);
                        review = reader.GetString(4);
                    }
                }
            }

            return new SongReview(songId, username, score, review) {Id = id};
        }

        public List<Song> GetSongsWithGenre(int genreId)
        {
            var songs = new List<Song>();

            using (SqlConnection conn = new SqlConnection(SqlManager.ConnectionString))
            {
                using (SqlCommand query = new SqlCommand(string.Format(QueryGetSongIdsWithGenre, genreId), conn))
                {
                    conn.Open();

                    using (var reader = query.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var songId = reader.GetInt32(0);
                            songs.Add(GetSong(songId));
                        }
                    }
                }
            }

            return songs;
        }
        
        public List<Song> GetArtistSongs(int artistId)
        {
            var songs = new List<Song>();

            using (SqlConnection conn = new SqlConnection(SqlManager.ConnectionString))
            {
                using (SqlCommand query = new SqlCommand(string.Format(QueryGetArtistSongs, artistId), conn))
                {
                    conn.Open();
                    var reader = query.ExecuteReader();

                    while (reader.Read())
                    {
                        songs.Add(GetSong(reader.GetInt32(0)));
                    }
                }
            }

            return songs;
        }

        public List<Song> GetSongs()
        {
            var songs = new List<Song>();

            using (SqlConnection conn = new SqlConnection(SqlManager.ConnectionString))
            {
                using (SqlCommand query = new SqlCommand(string.Format(QueryGetAllSongIds), conn))
                {
                    conn.Open();
                    var reader = query.ExecuteReader();

                    while (reader.Read())
                    {
                        songs.Add(GetSong(reader.GetInt32(0)));
                    }
                }
            }

            return songs;
        }
        
        public void UpdateReview(SongReview songReview)
        {
            if (GetReviewId(songReview.SongId, songReview.Username) == 0) return;

            using (SqlConnection conn = new SqlConnection(SqlManager.ConnectionString))
            {
                using (SqlCommand query = new SqlCommand(string.Format(QueryUpdateReview, GetReviewId(songReview.SongId, songReview.Username), songReview.Score, _sqlManager.GetSqlString(songReview.Review)), conn))
                {
                    conn.Open();
                    var reader = query.ExecuteReader();
                    reader.Close();
                }
            }
        }

        public void DeleteSong(int id)
        {
            using (SqlConnection conn = new SqlConnection(SqlManager.ConnectionString))
            {
                using (SqlCommand query = new SqlCommand(string.Format(QueryDeleteSongReviews, id), conn))
                {
                    conn.Open();
                    query.ExecuteNonQuery();
                }

                using (SqlCommand query = new SqlCommand(string.Format(QueryDeleteSongGenres, id), conn))
                {
                    query.ExecuteNonQuery();
                }

                using (SqlCommand query = new SqlCommand(string.Format(QueryDeleteSongArtists, id), conn))
                {
                    query.ExecuteNonQuery();
                }

                using (SqlCommand query = new SqlCommand(string.Format(QueryDeleteSongs, id), conn))
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
            if (!SongExistsInDb(single))
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
            if (!SongExistsInDb(track))
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
