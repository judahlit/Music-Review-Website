using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Music_Review_Application_GUI.Models
{
    public class AlbumViewModel
    {
        public int AlbumId { get; set; }

        [Required]
        public string Title { get; set; }

        public List<SongViewModel> Tracks { get; set; } = new();

        [Display(Name = "Release Date")]
        [DataType(DataType.Date)]
        public DateTime ReleaseDate { get; set; }

        public List<string> ArtistNames { get; set; } = new();

        public string ImgPath { get; set; }
    }
}
