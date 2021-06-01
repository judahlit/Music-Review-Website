using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Music_Review_Application_Models;
using ImageConverter = Music_Review_Application_Models.ImageConverter;

namespace Music_Review_Application_DB_Managers
{
    public class ArtistDbManager
    {
        #region Constants and Fields


        private const string QueryAddArtist = "INSERT INTO artist(artistName, img, description) VALUES('{0}',CONVERT(VARBINARY(MAX), '{1}'),'{2}');";
        private const string QueryGetArtistId = "SELECT id FROM artist WHERE artistName = '{0}';";
        private const string QueryGetArtist = "SELECT * FROM artist WHERE id = '{0}'";
        private const string QueryGetAllArtists = "";
        private const string QueryGetSortedArtists = "";

        private readonly SqlManager _sqlManager = new();
        private readonly ImageConverter _imageConverter = new();

        #endregion

        public ArtistDbManager()
        {

        }

        #region Methods

        public void AddArtist(Artist artist)
        {
            using (SqlConnection conn = new SqlConnection(SqlManager.ConnectionString))
            {
                using (SqlCommand query = new SqlCommand(string.Format(QueryAddArtist, _sqlManager.GetSqlString(artist.ArtistName), _imageConverter.ImageToByteArray(artist.Img), _sqlManager.GetSqlString(artist.Description)), conn))
                {
                    conn.Open();
                    query.ExecuteNonQuery();
                }
            }
        }

        public int GetArtistId(string artistName)
        {
            using (SqlConnection conn = new SqlConnection(SqlManager.ConnectionString))
            {
                using (SqlCommand query = new SqlCommand(string.Format(QueryGetArtistId, _sqlManager.GetSqlString(artistName)), conn))
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
        public Artist GetArtist(int id)
        {
            using (SqlConnection conn = new SqlConnection(SqlManager.ConnectionString))
            {
                using (SqlCommand query = new SqlCommand(string.Format(QueryGetArtist, id), conn))
                {
                    conn.Open();
                    var reader = query.ExecuteReader();

                    int artistId = 0;
                    string artistName = "";
                    Image img = null;
                    string description = "";

                    while (reader.Read())
                    {
                        artistId = reader.GetInt32(0);
                        artistName = reader.GetString(1);
                        img = _imageConverter.ByteArrayToImage((byte[])reader["img"]);
                        description = reader.GetString(3);
                    }

                    Artist artist = new(artistName, img, description) { Id = artistId };
                    return artist;
                }
            }
        }

        /*
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
