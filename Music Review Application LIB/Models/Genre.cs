using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music_Review_Application_LIB.Models
{
    public class Genre
    {
        public int Id { get; set; }
        public string GenreName { get; set; }

        public Genre(string genreName)
        {
            GenreName = genreName;
        }
    }
}
