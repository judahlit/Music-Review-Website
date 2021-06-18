using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;

namespace Music_Review_Application_GUI.Models
{
    public class ArtistViewModel
    {
        public int Id { get; set; }

        public string ArtistName { get; set; }

        public Image Img { get; set; }

        public string Description { get; set; }

        public List<SongViewModel> Songs { get; set; }

        public List<AlbumViewModel> Albums { get; set; }
    }
}
