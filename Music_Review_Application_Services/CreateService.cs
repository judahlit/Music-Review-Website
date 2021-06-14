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
        private readonly IAlbumDbManager _albumDbManager;

        public CreateService(ISongDbManager songDbManager, IAlbumDbManager albumDbManager)
        {
            _songDbManager = songDbManager;
            _albumDbManager = albumDbManager;
        }

        public string CreateSingle(string title, List<string> artistNames, List<string> genreNames, string dateDay, string dateMonth, string dateYear, string imgPath)
        {
            var lists = new List<List<string>> {artistNames, genreNames};
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

        public string CreateAlbum(string title, List<Track> tracks, List<string> artistNames, string dateDay, string dateMonth, string dateYear, string imgPath)
        {
            var lists = new List<List<string>> { artistNames };
            var strings = new List<string> { title, dateDay, dateMonth, dateYear };

            if (!AreListsValid(lists) || strings.Any(string.IsNullOrEmpty))
            {
                return "Please fill in the album data.";
            }

            var tempDate = ToDateTime(dateDay, dateMonth, dateYear);
            if (tempDate == null) return "Please fill in a valid date of release.";

            var correctDate = (DateTime)tempDate;

            if (!TracksAreValid(tracks))
            {
                return "Please fill in the track data correctly as well.";
            }

            Image img = null;
            var album = new Album(title, tracks, correctDate, img, artistNames);

            if (_albumDbManager.AlbumExistsInDb(album))
            {
                return "This song already is on the website.";
            }

            var albumAdded = _albumDbManager.AlbumIsAdded(album);

            if (albumAdded)
            {
                return "Album was added to the website.";
            }
            else
            {
                return "Album couldn't be added to the webiste.";
            }
        }

        public string CreateSongReview(int songId, string username, int score, string review)
        {
            throw new NotImplementedException();
        }

        public string CreateAlbumReview(int albumId, string username, int score, string review)
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

        private bool TracksAreValid(List<Track> tracks)
        {
            if (tracks.Count == 0) return false;

            var titles = tracks.Select(t => t.Title).ToList();

            if (titles.Any(string.IsNullOrEmpty)) return false;

            foreach (var track in tracks)
            {
                var genreNames = track.Genres.Select(g => g.GenreName).ToList();
                var lists = new List<List<string>> { track.ArtistNames, genreNames };

                if (!AreListsValid(lists) || string.IsNullOrEmpty(track.Title))
                {
                    return false;
                }
            }

            return true;
        }
    }
}
