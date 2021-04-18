using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music_Review_Application_LIB
{
    public class Album
    {
        #region Properties

        public int Id { get; set; }

        public string Title { get; set; }

        public List<Song> Songs { get; set; }

        public DateTime DateOfRelease { get; set; }

        public double Score { get; set; }

        public string Img { get; set; }

        public List<string> ArtistNames { get; set; }

        public List<string> GenreNames { get; set; }

        #endregion

        public Album(string title, List<Song> songs, DateTime dateOfRelease, string img, List<string> genreNames)
        {
            Title = title;
            Songs = songs;
            DateOfRelease = dateOfRelease;
            Img = img;
            GenreNames = genreNames;
        }
    }
}
