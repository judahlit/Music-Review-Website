using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Music_Review_Application_Models;

namespace Music_Review_Application_DB_Managers
{
    public class GenreDbManager
    {
        #region Constants and Fields

        private const string QueryAddGenre = "INSERT INTO Genre(genreName) VALUES('{0}');";
        private const string QueryGetGenreById = "SELECT * FROM Genre WHERE id = {0};";
        private const string QueryGetGenreByGenreName = "SELECT * FROM Genre WHERE genreName = '{0}';";
        private const string QueryGetGenreId = "SELECT id FROM Genre WHERE genreName = '{0}';";

        private readonly AppManager _appManager = new();

        #endregion


        #region Methods

        public void AddGenre(Genre genre)
        {
            using (SqlConnection conn = new SqlConnection(AppManager.ConnectionString))
            {
                using (SqlCommand query = new SqlCommand(string.Format(QueryAddGenre, _appManager.GetSqlString(genre.GenreName)), conn))
                {
                    conn.Open();
                    query.ExecuteNonQuery();
                }
            }
        }
        public void AddGenre(string genreName)
        {
            using (SqlConnection conn = new SqlConnection(AppManager.ConnectionString))
            {
                using (SqlCommand query = new SqlCommand(string.Format(QueryAddGenre, _appManager.GetSqlString(genreName)), conn))
                {
                    conn.Open();
                    query.ExecuteNonQuery();
                }
            }
        }

        public Genre GetGenre(int genreId)
        {
            using (SqlConnection conn = new SqlConnection(AppManager.ConnectionString))
            {
                using (SqlCommand query = new SqlCommand(string.Format(QueryGetGenreById, genreId), conn))
                {
                    conn.Open();
                    var reader = query.ExecuteReader();

                    string genreName = "";

                    while (reader.Read())
                    {
                        genreName = reader.GetString(1);
                    }

                    Genre genre = new(genreName) { Id = genreId };
                    return genre;
                }
            }
        }

        public Genre GetGenre(string genreName)
        {
            using (SqlConnection conn = new SqlConnection(AppManager.ConnectionString))
            {
                using (SqlCommand query = new SqlCommand(string.Format(QueryGetGenreByGenreName, genreName), conn))
                {
                    conn.Open();
                    var reader = query.ExecuteReader();

                    int genreId = 0;

                    while (reader.Read())
                    {
                        genreId = reader.GetInt32(0);
                    }

                    Genre genre = new(genreName) { Id = genreId };
                    return genre;
                }
            }
        }

        public int GetGenreId(string genreName)
        {
            using (SqlConnection conn = new SqlConnection(AppManager.ConnectionString))
            {
                using (SqlCommand query = new SqlCommand(string.Format(QueryGetGenreId, _appManager.GetSqlString(genreName)), conn))
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

        #endregion
    }
}
