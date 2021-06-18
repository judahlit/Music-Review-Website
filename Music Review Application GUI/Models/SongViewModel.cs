using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Music_Review_Application_GUI.Pages.Forms;

namespace Music_Review_Application_GUI.Models
{
    public class SongViewModel
    {
        public int SongId { get; private set; }
        public int AlbumId { get; set; }
        public string Title { get; set; }
        public List<string> ArtistNames { get; set; } = new();
        public List<string> GenreNames { get; set; } = new();
        public string DateDay { get; set; }
        public string DateMonth { get; set; }
        public string DateYear { get; set; }
        public string ImgPath { get; set; }
    }
}
