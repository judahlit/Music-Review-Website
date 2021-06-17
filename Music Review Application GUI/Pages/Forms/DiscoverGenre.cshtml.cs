using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Music_Review_Application_DB_Managers.Interfaces;
using Music_Review_Application_Models;
using Music_Review_Application_Sample_Data;

namespace Music_Review_Application_GUI.Pages.Forms
{
    public class DiscoverGenreModel : PageModel
    {
        private readonly ISongDbManager _songDbManager;
        private readonly IAlbumDbManager _albumDbManager;
        private readonly IGenreDbManager _genreDbManager;

        public static Genre CurrentGenre { get; set; }
        public static List<Song> Songs { get; set; } = new();
        public static List<Album> Albums { get; set; } = new();
        public static List<Genre> Genres { get; set; } = new();
        [BindProperty]
        public int ChosenGenreId { get; set; }

        public DiscoverGenreModel(ISongDbManager songDbManager, IAlbumDbManager albumDbManager, IGenreDbManager genreDbManager)
        {
            _songDbManager = songDbManager;
            _albumDbManager = albumDbManager;
            _genreDbManager = genreDbManager;
        }

        public void OnGet(int genreId)
        {
            CurrentGenre = _genreDbManager.GetGenre(genreId);
            Songs.Clear();
            Albums.Clear();
            Genres.Clear();
            var receivedSongs = _songDbManager.GetSongsWithGenre(genreId);
            var receivedAlbums = new List<Album>();
            var receivedGenres = _genreDbManager.GetGenres();

            foreach (var song in receivedSongs)
            {
                var titles = Songs.Select(s => s.Title).ToList();
                if (!titles.Contains(song.Title)) Songs.Add(song);
                if (song.GetType() == typeof(Track))
                {
                    var track = (Track)song;
                    if (track.AlbumId > 0)
                    {
                        receivedAlbums.Add(_albumDbManager.GetAlbum(track.AlbumId));
                    }
                }
            }

            foreach (var album in receivedAlbums)
            {
                var titles = Albums.Select(a => a.Title).ToList();
                if (!titles.Contains(album.Title)) Albums.Add(album);
            }

            foreach (var genre in receivedGenres)
            {
                var genreNames = Genres.Select(g => g.GenreName).ToList();
                if (!genreNames.Contains(genre.GenreName) && !string.IsNullOrEmpty(genre.GenreName)) Genres.Add(genre);
            }
        }

        public IActionResult OnPost()
        {
            if (ChosenGenreId == 0) return RedirectToPage("/Forms/Discover");
            return RedirectToPage("/Forms/DiscoverGenre", new { genreId = ChosenGenreId });
        }
    }
}
