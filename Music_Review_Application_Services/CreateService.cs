using System;
using System.Collections.Generic;
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

        public string CreateSingle(string title, List<string> artistNames, List<string> genreNames, string dateDay, string dateMonth, string dateYear)
        {
            var lists = new List<List<string>>{artistNames, genreNames};
            var strings = new List<string> {title, dateDay, dateMonth, dateYear};

            if (AreListsValid(lists) || strings.Any(string.IsNullOrEmpty))
            {
                return "Please fill in all required fields.";
            }

            var tempDate = ToDateTime(dateDay, dateMonth, dateYear);
            if (tempDate == null) return "Please fill in a valid date of release.";

            var correctDate
            var singleAdded = _songDbManager.SingleIsAdded(new SingleSong(title))

            return "Song was added to the website.";
        }

        public string CreateSingle(string title, List<string> artistNames, List<string> genreNames, string dateDay, string dateMonth, string dateYear,
            string imgPath)
        {
            throw new NotImplementedException();
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
            DateTime correctDate;

            if (DateTime.TryParse($"{dateDay}-{dateMonth}-{dateYear}", out correctDate) && DateTime.Compare(correctDate, DateTime.Now) <= 0)
            {
                return correctDate;
            }

            return null;
        }
    }
}
