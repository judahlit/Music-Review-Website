using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Music_Review_Application_LIB;

namespace Music_Review_Application_GUI.Models
{
    public class AddSongModel
    {
        #region Properties

        public int SongId { get; private set; }
        public int AlbumId { get; set; }
        public string Title { get; private set; }
        public string Artist { get; private set; }
        public DateTime Date { get; set; }

        #endregion

        #region Constructor

        public AddSongModel(string title, string artist, DateTime date)
        {
            Title = title;
            Artist = artist;
            Date = date;
        }

        #endregion

        #region Methods

        public void AddSingleToDB()
        {
            Song song = new Song(Title, Artist, Date);
        }

        #endregion
    }
}
