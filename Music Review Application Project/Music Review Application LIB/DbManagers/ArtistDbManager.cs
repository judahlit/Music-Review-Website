using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music_Review_Application_LIB.DbManagers
{
    public class ArtistDbManager
    {
        #region Constants and Fields

        private const string QueryAddArtist = "INSERT INTO artist(artistName, img, description) VALUES('{0}','{1}','{2}');";
        private const string QueryGetArtistId = "SELECT id FROM artist WHERE artistName = '{0}';";
        private const string QueryGetArtistById = "SELECT * FROM artist WHERE id = '{0}'";
        private const string QueryGetArtistByArtistName = "";
        private const string QueryGetAllArtists = "";
        private const string QueryGetSortedArtists = "";


        #endregion

        public ArtistDbManager()
        {

        }

        #region Methods

        public void AddArtist(Artist artist)
        {

        }

        public int GetArtistId(string artistName)
        {
            using (SqlConnection conn = new SqlConnection(AppManager.ConnectionString))
            {
                using (SqlCommand query = new SqlCommand(string.Format(QueryGetArtistId, artistName), conn))
                {
                    conn.Open();

                    var reader = query.ExecuteReader();
                    return reader.GetInt32(0);
                }
            }
        }
        /*
        public Artist GetArtist(int id)
        {
        }

        public Artist GetArtist(string artistName)
        {

        }

        public List<Artist> GetArtists()
        {

        }

        public List<Artist> GetArtists(bool byAvgScore, List<string> madeGenres)
        {

        }
        */
        #endregion
    }
}
