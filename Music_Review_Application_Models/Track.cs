using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music_Review_Application_Models
{
    public class Track : Song
    {
        public int TrackNr { get; set; }

        public int AlbumId { get; set; }

        public Track(string title, DateTime date, int trackNr, List<string> artistNames, List<Genre> genres)
            : base(title, date, artistNames, genres)
        {
            TrackNr = trackNr;
        }
    }
}
