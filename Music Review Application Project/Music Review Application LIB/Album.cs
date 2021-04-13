using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music_Review_Application_LIB
{
    public class Album
    {
        #region Constants and Fields

        private const string _queryAddAlbum = "INSERT INTO album(title, dateOfRelease, score, img) VALUES('{0}','{1}',{2},{3});";
        private const string _queryAddAlbumArtist = "INSERT INTO albumArtist(albumId, artistId) VALUES({0},{1});";
        private const string _queryGetAlbumId = "SELECT id FROM album WHERE title = '{0}' AND dateOfRelease = '{1}';";

        #endregion

        #region Properties



        #endregion
    }
}
