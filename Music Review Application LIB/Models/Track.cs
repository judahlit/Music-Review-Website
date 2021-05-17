using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music_Review_Application_LIB
{
    public class Track: Song
    {
        public int TrackId { get; private set; }

        public int AlbumId { get; private set; }

        public Track(string title, DateTime date, int trackId, int albumId, List<string> artistNames, List<string> genreNames)
            : base(title, date, artistNames, genreNames)
        {
            TrackId = trackId;
            AlbumId = albumId;
        }
    }
}
