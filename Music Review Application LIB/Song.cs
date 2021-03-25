using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music_Review_Application_LIB
{
    public class Song
    {
        #region Enums

        public enum SongType
        {
            Single,
            TrackOnAlbum
        }

        #endregion

        #region Properties

        public int SongId { get; private set; }
        public int AlbumId { get; set; }
        public int GenreId { get; set; }
        public string Title { get; private set; }
        public string Artist { get; private set; }
        public DateTime Date { get; set; }

        #endregion

        #region Constructor

        public Song (string title, string artist, int genreId, DateTime date)
        {
            Title = title;
            Artist = artist;
            GenreId = genreId;
            Date = date;
            GenerateId();
        }

        #endregion

        #region Methods

        private async Task GenerateId()
        {

        }

        #endregion
    }
}
