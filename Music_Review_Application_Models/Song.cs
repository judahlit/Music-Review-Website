using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Music_Review_Application_Models
{
    public class Song
    {
        #region Properties

        public int Id { get; set; }

        public string Title { get; set; }

        public DateTime DateOfRelease { get; set; }

        public double Score { get; set; }

        public List<string> ArtistNames { get; set; } = new();

        public List<Genre> Genres { get; set; } = new();

        #endregion

        protected Song(string title, DateTime date, List<string> artistNames, List<Genre> genres)
        {
            Title = title;
            DateOfRelease = date;
            ArtistNames = artistNames;
            Genres = genres;
        }
    }
}
