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
        #region Properties

        public int SongId { get; private set; }
        public int AlbumId { get; set; }
        public string Title { get; set; }
        public string Artist { get; set; }
        public string DateDay { get; set; }
        public string DateMonth { get; set; }
        public string DateYear { get; set; }
        public DateTime Date { get; set; }
        public string Genre { get; set; }

        #endregion

        #region Constructor

        public SongModel(string title, string artist, string dateDay, string dateMonth, string dateYear)
        {
            Title = title;
            Artist = artist;
            DateDay = dateDay;
            DateMonth = dateMonth;
            DateYear = dateYear;
        }

        #endregion

        #region Methods

        public void AddSingleToDB()
        {
            Song song = new Song(Title, Artist, 0, Date);
        }

        public void ToDateTime()
        {
            DateTime correctDate;

            if (DateTime.TryParse(DateYear + "-" + DateMonth + "-" + DateDay, out correctDate))
            {
                Date = correctDate;
            }
            else
            {
                AddSongModel.ErrorMessage = "Please fill in a valid date in the 'Date of Release' field.";
            }
        }

        #endregion
    }
}
