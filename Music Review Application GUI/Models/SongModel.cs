using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Music_Review_Application_GUI.Pages.Forms;
using Music_Review_Application_LIB;

namespace Music_Review_Application_GUI.Models
{
    public class SongModel
    {
        public int SongId { get; private set; }
        public int AlbumId { get; set; }
        public string Title { get; set; }
        public string Artist { get; set; }
        public string DateDay { get; set; }
        public string DateMonth { get; set; }
        public string DateYear { get; set; }
        public DateTime Date { get; set; }
    }
}
