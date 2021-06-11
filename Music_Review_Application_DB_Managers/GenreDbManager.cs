using System.Collections.Generic;
using System.Data.SqlClient;
using Music_Review_Application_DB_Managers.Interfaces;
using Music_Review_Application_Models;

namespace Music_Review_Application_DB_Managers
{
    public class GenreDbManager : IGenreDbManager
    {
        #region Constants and Fields

        private const string QueryAddGenre = "INSERT INTO Genre(genreName) VALUES('{0}');";
        private const string QueryGetGenreById = "SELECT * FROM Genre WHERE id = {0};";
        private const string QueryGetGenreByGenreName = "SELECT * FROM Genre WHERE genreName = '{0}';";
        private const string QueryGetGenreId = "SELECT id FROM Genre WHERE genreName = '{0}';";

        private readonly ISqlManager _sqlManager;

        #endregion

        public GenreDbManager(ISqlManager sqlManager)
        {
            _sqlManager = sqlManager;
        }

        #region Methods

        public void AddGenre(Genre genre)
        {
            using (SqlConnection conn = new SqlConnection(SqlManager.ConnectionString))
            {
                using (SqlCommand query = new SqlCommand(string.Format(QueryAddGenre, _sqlManager.GetSqlString(genre.GenreName)), conn))
                {
                    conn.Open();
                    query.ExecuteNonQuery();
                }
            }
        }
        public void AddGenre(string genreName)
        {
            using (SqlConnection conn = new SqlConnection(SqlManager.ConnectionString))
            {
                using (SqlCommand query = new SqlCommand(string.Format(QueryAddGenre, _sqlManager.GetSqlString(genreName)), conn))
                {
                    conn.Open();
                    query.ExecuteNonQuery();
                }
            }
        }

        public Genre GetGenre(int genreId)
        {
            using (SqlConnection conn = new SqlConnection(SqlManager.ConnectionString))
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
            using (SqlConnection conn = new SqlConnection(SqlManager.ConnectionString))
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
            using (SqlConnection conn = new SqlConnection(SqlManager.ConnectionString))
            {
                using (SqlCommand query = new SqlCommand(string.Format(QueryGetGenreId, _sqlManager.GetSqlString(genreName)), conn))
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

        public void CheckGenres(List<Genre> genres)
        {
            foreach (Genre genre in genres)
            {
                if (GetGenreId(genre.GenreName) == 0)
                {
                    AddGenre(genre);
                }
            }
        }

        #endregion
    }
}
