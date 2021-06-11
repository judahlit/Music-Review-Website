using System.Collections.Generic;
using System.Data.SqlClient;
using Music_Review_Application_DB_Managers.Interfaces;
using Music_Review_Application_Models;

namespace Music_Review_Application_DB_Managers
{
    public class UserListDbManager : IUserListDbManager
    {
        private const string QueryGetSongReviews = "SELECT * FROM SongReview WHERE username = '{0}';";
        private const string QueryGetAlbumReviews = "SELECT * FROM AlbumReview WHERE username = '{0}';";

        private const string QueryDeleteSongReviews = "DELETE FROM SongReview WHERE username = '{0}';";
        private const string QueryDeleteAlbumReviews = "DELETE FROM AlbumReview WHERE username = '{0}';";

        private readonly ISqlManager _sqlManager;
        private readonly ISongDbManager _songDbManager;
        private readonly IAlbumDbManager _albumDbManager;

        public UserListDbManager(ISqlManager sqlManager, ISongDbManager songDbManager, IAlbumDbManager albumDbManager)
        {
            _sqlManager = sqlManager;
            _songDbManager = songDbManager;
            _albumDbManager = albumDbManager;
        }

        public UserList GetUserList(string username)
        {
            var reviewedSongs = new List<SongReview>();
            var reviewedAlbums = new List<AlbumReview>();

            using (SqlConnection conn = new SqlConnection(SqlManager.ConnectionString))
            {
                using (SqlCommand query = new SqlCommand(string.Format(QueryGetSongReviews, username), conn))
                {
                    conn.Open();

                    using (var reader = query.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var songId = reader.GetInt32(1);
                            var score = reader.GetInt32(3);
                            var review = reader.GetString(4);
                            reviewedSongs.Add(new SongReview(songId, username, score, review));
                        }
                    }
                }

                using (SqlCommand query = new SqlCommand(string.Format(QueryGetAlbumReviews, username), conn))
                {
                    using (var reader = query.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var albumId = reader.GetInt32(1);
                            var score = reader.GetInt32(3);
                            var review = reader.GetString(4);
                            reviewedAlbums.Add(new AlbumReview(albumId, username, score, review));
                        }
                    }
                }
            }

            return new UserList(username, reviewedSongs, reviewedAlbums);
        }

        public void DeleteUserReviews(string username)
        {
            using (SqlConnection conn = new SqlConnection(SqlManager.ConnectionString))
            {
                using (SqlCommand query = new SqlCommand(string.Format(QueryDeleteSongReviews, username), conn))
                {
                    conn.Open();
                    query.ExecuteNonQuery();
                }

                using (SqlCommand query = new SqlCommand(string.Format(QueryDeleteAlbumReviews, username), conn))
                {
                    query.ExecuteNonQuery();
                }
            }
        }
    }
}
