using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music_Review_Application_Models
{
    public class SingleSong : Song
    {
        public Image Img { get; private set; }

        public SingleSong(string title, DateTime date, Image img, List<string> artistNames, List<Genre> genres)
            : base(title, date, artistNames, genres)
        {
            Img = img;
        }
    }
}
