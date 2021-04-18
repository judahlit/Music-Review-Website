using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music_Review_Application_LIB
{
    public class Song
    {
        #region Properties

        public int Id { get; private set; }

        public string Title { get; private set; }

        public DateTime DateOfRelease { get; private set; }

        public int? TrackId { get; private set; }

        public double Score { get; set; }

        public int? AlbumId { get; private set; }

        public string Img { get; private set; }

        public List<Artist> ArtistNames { get; private set; } = new List<Artist>();

        public List<string> GenreNames { get; private set; } = new List<string>();

        #endregion


        #region Constructors

        public Song(string title, DateTime date, int trackId, int albumId, List<Artist> artistNames, List<string> genreNames)
        {
            Title = title;
            DateOfRelease = date;
            TrackId = trackId;
            AlbumId = albumId;
            ArtistNames = artistNames;
            GenreNames = genreNames;
        }

        public Song(string title, DateTime date, string img, List<Artist> artistNames, List<string> genreNames)
        {
            Title = title;
            DateOfRelease = date;
            Img = img;
            ArtistNames = artistNames;
            GenreNames = genreNames;
        }

        #endregion

        public bool IsSingle()
        {
            if (TrackId == null)
            {
                return true;
            }

            return false;
        }
    }
}
