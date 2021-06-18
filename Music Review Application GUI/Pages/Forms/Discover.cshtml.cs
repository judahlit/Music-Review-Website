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
    public class DiscoverModel : PageModel
    {
        private readonly ISongDbManager _songDbManager;
        private readonly IAlbumDbManager _albumDbManager;
        private readonly IGenreDbManager _genreDbManager;

        public static List<Song> Songs { get; private set; } = new();
        public static List<Album> Albums { get; private set; } = new();
        public static List<Genre> Genres { get; private set; } = new();
        [BindProperty]
        public int ChosenGenreId { get; set; }

        public DiscoverModel(ISongDbManager songDbManager, IAlbumDbManager albumDbManager, IGenreDbManager genreDbManager)
        {
            _songDbManager = songDbManager;
            _albumDbManager = albumDbManager;
            _genreDbManager = genreDbManager;
        }

        public void OnGet()
        {
            Songs.Clear();
            Songs = _songDbManager.GetSongs();
            Albums.Clear();
            Albums = _albumDbManager.GetAlbums();
            Genres.Clear();
            Genres = _genreDbManager.GetGenres();
        }

        public IActionResult OnPost()
        {
            if (ChosenGenreId == 0) return Page();
            return RedirectToPage("/Forms/DiscoverGenre", new { genreId = ChosenGenreId });
        }
    }
}
