using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music_Review_Application_LIB.DbManagers
{
    public class AlbumDbManager
    {
        #region Constants and Fields

        private const string QueryAddAlbum = "INSERT INTO album(title, dateOfRelease, score, img) VALUES('{0}','{1}',{2},{3});";
        private const string QueryAddAlbumArtist = "INSERT INTO albumArtist(albumId, artistId) VALUES({0},{1});";
        private const string QueryGetAlbumId = "SELECT id FROM album WHERE title = '{0}' AND dateOfRelease = '{1}';";
        private const string QueryGetAlbumById = "";
        private const string QueryGetAlbumByTitleAndDate = "";
        private const string QueryGetAllAlbums = "";
        private const string QueryGetSortedAlbums = "";
        private const string QueryUpdateScore = "";
        private const string QueryGetUserScore = "";
        private const string QueryDeleteAlbum = "";

        private readonly AppManager _appManager = new();

        #endregion

        public AlbumDbManager()
        {

        }

        #region Methods

        public void AddAlbum(Album album)
        {

        }
        
        public int GetAlbumId(string title, DateTime dateOfRelease)
        {
            using (SqlConnection conn = new SqlConnection(AppManager.ConnectionString))
            {
                using (SqlCommand query = new SqlCommand(string.Format(QueryGetAlbumId, _appManager.GetSqlString(title), dateOfRelease), conn))
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

        /*

        public Album GetAlbum(int id)
        {

        }

        public Album GetAlbum(string title, DateTime dateOfRelease)
        {

        }

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

        public void DeleteAlbum(int id)
        {

        }
        */
        #endregion
    }
}
