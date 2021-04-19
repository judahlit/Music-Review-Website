using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music_Review_Application_LIB.DbManagers
{
    public class SongDbManager
    {
        #region Constants and Fields

        private const string QueryAddSingleInSongTable = "INSERT INTO Song(title, dateOfRelease) VALUES('{0}','{1}');";
        private const string QueryAddTrack = "INSERT INTO Song(title, dateOfRelease, trackId, albumId) VALUES('{0}','{1}',{2},{3});";
        private const string QueryAddSingle = "INSERT INTO Single(songId, img) VALUES({0},CONVERT(VARBINARY(MAX),'{1}'));";
        private const string QueryAddSongArtist = "INSERT INTO SongArtist(songId, artistId) VALUES({0},{1});";
        private const string QueryAddSongGenre = "INSERT INTO SongGenre(songId, genre) VALUES({0},'{1}');";
        private const string QueryGetSongId = "SELECT id FROM Song WHERE title = '{0}' AND dateOfRelease = '{1}';";
        private const string QueryGetSongById = "SELECT * FROM Song WHERE id = {0};";
        private const string QueryGetSingleBySongId = "SELECT * FROM Single WHERE songId = {0};";
        private const string QueryGetSongArtistsBySongId = "SELECT * FROM SongArtist WHERE songId = {0};";
        private const string QueryGetSongGenresBySongId = "SELECT * FROM SongGenre WHERE songId = {0};";

        private const string QueryGetAllSongs = "";
        private const string QueryGetSortedSongs = "";
        private const string QueryUpdateScore = "";
        private const string QueryGetUserScore = "";
        private const string QueryDeleteSong = "";

        #endregion

        public SongDbManager()
        {

        }

        #region Methods

        public void AddSingle(Single single)
        {
            using (SqlConnection conn = new SqlConnection(AppManager.ConnectionString))
            {
                using (SqlCommand query = new SqlCommand(string.Format(QueryAddSingleInSongTable, AppManager.GetSqlString(single.Title), single.DateOfRelease), conn))
                {
                    conn.Open();
                    query.ExecuteNonQuery();
                }

                using (SqlCommand query = new SqlCommand(string.Format(QueryAddSingle, GetSongId(single.Title, single.DateOfRelease), single.Img), conn))
                {
                    query.ExecuteNonQuery();
                }

                foreach (string artistName in single.ArtistNames)
                {
                    ArtistDbManager artistDbManager = new();

                    using (SqlCommand query = new SqlCommand(string.Format(QueryAddSongArtist, GetSongId(single.Title, single.DateOfRelease), artistDbManager.GetArtistId(artistName)), conn))
                    {
                        query.ExecuteNonQuery();
                    }
                }

                foreach (string genreName in single.GenreNames)
                {
                    using (SqlCommand query = new SqlCommand(string.Format(QueryAddSongGenre, GetSongId(single.Title, single.DateOfRelease), genreName), conn))
                    {
                        query.ExecuteNonQuery();
                    }
                }
            }
        }

        public void AddTrack(Song track)
        {

        }

        public int GetSongId(string title, DateTime dateOfRelease)
        {
            using (SqlConnection conn = new SqlConnection(AppManager.ConnectionString))
            {
                using (SqlCommand query = new SqlCommand(string.Format(QueryGetSongId, AppManager.GetSqlString(title), dateOfRelease), conn))
                {
                    conn.Open();
                    var reader = query.ExecuteReader();

                    if (reader.Read())
                    {
                        return reader.GetInt32(0);
                    }

                    return 0;
                }
            }
        }
        
        public Single GetSingle(int id)
        {
            string title = null;
            DateTime dateOfRelease = new();
            double score = 0;
            byte[] img = null;
            List<string> artistNames = new();
            List<string> genreNames = new();

            using (SqlConnection conn = new SqlConnection(AppManager.ConnectionString))
            {
                using (SqlCommand query = new SqlCommand(string.Format(QueryGetSongById, id), conn))
                {
                    conn.Open();
                    var reader = query.ExecuteReader();

                    while (reader.Read())
                    {
                        title = reader.GetString(1);
                        dateOfRelease = (DateTime)reader["dateOfRelease"];

                        if (reader["score"] != DBNull.Value)
                        {
                            score = (double)reader["score"];
                        }
                    }

                    reader.Close();
                }

                using (SqlCommand query = new SqlCommand(string.Format(QueryGetSingleBySongId, id), conn))
                {
                    var reader = query.ExecuteReader();

                    if (reader.Read())
                    {
                        img = (byte[])reader["img"];
                    }

                    reader.Close();
                }

                using (SqlCommand query = new SqlCommand(string.Format(QueryGetSongArtistsBySongId, id), conn))
                {
                    var reader = query.ExecuteReader();
                    ArtistDbManager artistDbManager = new();

                    while (reader.Read())
                    {
                        artistNames.Add(artistDbManager.GetArtist(reader.GetInt32(2)).ArtistName);
                    }

                    reader.Close();
                }

                using (SqlCommand query = new SqlCommand(string.Format(QueryGetSongGenresBySongId, id), conn))
                {
                    var reader = query.ExecuteReader();

                    while (reader.Read())
                    {
                        genreNames.Add(reader.GetString(2));
                    }

                    reader.Close();
                }
            }

            Single single = new(title, dateOfRelease, img, artistNames, genreNames);
            single.Id = id;
            single.Score = score;

            return single;
        }

        /*
        public Song GetSong(string title, DateTime dateOfRelease)
        {

        }

        public List<Song> GetSongs()
        {
            
        }

        public List<Song> GetSong(string title, bool byDate, bool byScore, List<string> genres)
        {

        }

        public SongScore GetUserScore(Song song, string username)
        {

        }

        public void UpdateScore(int songId, SongScore userScore)
        {

        }

        public void DeleteSong(int id)
        {

        }*/


        #endregion
    }
}
