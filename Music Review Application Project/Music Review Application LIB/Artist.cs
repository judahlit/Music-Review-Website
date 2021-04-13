using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music_Review_Application_LIB
{
    public class Artist
    {
        #region Constants and Fields

        private const string _queryAddArtist = "INSERT INTO artist(artistName, img, description) VALUES('{0}','{1}','{2}');";

        #endregion


        #region Properties

        public int Id { get; set; }

        public string ArtistName { get; set; }

        public string Image { get; set; }

        public string Description { get; set; }

        #endregion

        #region Constructors

        public Artist(string artistName, string image, string description)
        {
            ArtistName = artistName;
            Image = image;
            Description = description;
            AddArtistToDB();
        }

        private void AddArtistToDB()
        {
            GetId();
            throw new NotImplementedException();
        }

        private async Task GetId()
        {

        }

        #endregion
    }
}
