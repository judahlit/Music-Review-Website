using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Music_Review_Application_DB_Managers.Interfaces;
using Music_Review_Application_Models;
using Music_Review_Application_Sample_Data;
using Music_Review_Application_Services.Interfaces;

namespace Music_Review_Application_GUI.Pages.Forms
{
    public class DiscoverGenreModel : PageModel
    {
        private readonly ISongDbManager _songDbManager;
        private readonly IGenreDbManager _genreDbManager;
        private readonly IAlbumService _albumService;

        public static Genre CurrentGenre { get; set; }
        public static List<Song> Songs { get; set; } = new();
        public static List<Album> Albums { get; set; } = new();
        public static List<Genre> Genres { get; set; } = new();
        [BindProperty]
        public int ChosenGenreId { get; set; }

        public DiscoverGenreModel(ISongDbManager songDbManager, IGenreDbManager genreDbManager, IAlbumService albumService)
        {
            _songDbManager = songDbManager;
            _genreDbManager = genreDbManager;
            _albumService = albumService;
        }

        public void OnGet(int genreId)
        {
            Songs.Clear();
            Albums.Clear();
            Genres.Clear();
            CurrentGenre = _genreDbManager.GetGenre(genreId);
            Songs = _songDbManager.GetSongsWithGenre(genreId);
            Albums = _albumService.GetAlbumsFromGenreSongs(Songs);
            Genres = _genreDbManager.GetGenres();
        }

        public IActionResult OnPost()
        {
            if (ChosenGenreId == 0) return RedirectToPage("/Forms/Discover");
            return RedirectToPage("/Forms/DiscoverGenre", new { genreId = ChosenGenreId });
        }
    }
}
