using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music_Review_Application_Models
{
    public class Artist
    {
        #region Properties

        public int Id { get; set; }

        public string ArtistName { get; set; }

        public Image Img { get; set; }

        public string Description { get; set; }

        #endregion

        public Artist(string artistName, Image img, string description)
        {
            ArtistName = artistName;
            Img = img;
            Description = description;
        }
    }
}
