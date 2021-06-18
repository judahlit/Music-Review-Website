using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Music_Review_Application_GUI.Models
{
    public class AlbumViewModel
    {
        public int AlbumId { get; set; }
        public string Title { get; set; }
        public List<SongViewModel> Tracks { get; set; } = new();
        public string DateDay { get; set; }
        public string DateMonth { get; set; }
        public string DateYear { get; set; }
        public List<string> ArtistNames { get; set; } = new();
        public string ImgPath { get; set; }
    }
}
