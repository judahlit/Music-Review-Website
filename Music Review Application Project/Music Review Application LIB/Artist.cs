using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music_Review_Application_LIB
{
    public class Artist
    {
        #region Properties

        public int Id { get; set; }

        public string ArtistName { get; set; }

        public string Img { get; set; }

        public string Description { get; set; }

        #endregion

        public Artist(string artistName, string img, string description)
        {
            ArtistName = artistName;
            Img = img;
            Description = description;
        }
    }
}
