using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music_Review_Application_LIB
{
    public class Single: Song
    {
        public byte[] Img { get; private set; }

        public Single(string title, DateTime date, byte[] img, List<string> artistNames, List<string> genres)
        :base(title, date, artistNames, genres)
        {
            
        }
    }
}
