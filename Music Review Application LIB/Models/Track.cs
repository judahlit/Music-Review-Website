using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Music_Review_Application_LIB.Models;

namespace Music_Review_Application_LIB
{
    public class Track: Song
    {
        public int TrackId { get; set; }

        public int AlbumId { get; set; }

        public Track(string title, DateTime date, int trackId, List<string> artistNames, List<Genre> genres)
            : base(title, date, artistNames, genres)
        {
            TrackId = trackId;
        }
    }
}
