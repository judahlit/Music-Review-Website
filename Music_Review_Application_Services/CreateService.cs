using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using Music_Review_Application_DB_Managers.Interfaces;
using Music_Review_Application_Models;
using Music_Review_Application_Services.Interfaces;

namespace Music_Review_Application_Services
{
    public class CreateService: ICreateService
    {
        private readonly ISongDbManager _songDbManager;

        public CreateService(ISongDbManager songDbManager)
        {
            _songDbManager = songDbManager;
        }

        public string CreateSingle(string title, List<string> artistNames, List<string> genreNames, string dateDay, string dateMonth, string dateYear, string imgPath)
        {
            var lists = new List<List<string>>{artistNames, genreNames};
            var strings = new List<string> {title, dateDay, dateMonth, dateYear};

            if (!AreListsValid(lists) || strings.Any(string.IsNullOrEmpty))
            {
                return "Please fill in all required fields.";
            }

            var tempDate = ToDateTime(dateDay, dateMonth, dateYear);
            if (tempDate == null) return "Please fill in a valid date of release.";

            var correctDate = (DateTime)tempDate;
            var genres = new List<Genre>();
            Image img = null;

            foreach (string genreName in genreNames)
            {
                genres.Add(new Genre(genreName));
            }

            var single = new SingleSong(title, correctDate, img, artistNames, genres);

            if (_songDbManager.SongExistsInDb(single))
            {
                return "This song already is on the website.";
            }

            var singleAdded = _songDbManager.SingleIsAdded(single);

            if (singleAdded)
            {
                return "Song was added to the website.";
            }
            else
            {
                return "Song couldn't be added to the webiste.";
            }
        }

        private bool AreListsValid(List<List<string>> lists)
        {
            foreach (var list in lists)
            {
                if (list.Count == 0) return false;
                if (string.IsNullOrEmpty(list[0])) return false;
            }

            return true;
        }

        private DateTime? ToDateTime(string dateDay, string dateMonth, string dateYear)
        {
            var correctDate = new DateTime();
            var culture = CultureInfo.CreateSpecificCulture("nl-NL");
            var styles = DateTimeStyles.AssumeLocal;

            if (DateTime.TryParse($"{dateDay}/{dateMonth}/{dateYear}", culture, styles, out correctDate))
            {
                if (DateTime.Compare(correctDate, DateTime.Now) <= 0)
                {
                    return correctDate;
                }
            }

            return null;
        }
    }
}
