using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Music_Review_Application_DB_Managers.Interfaces;
using Music_Review_Application_Models;
using Music_Review_Application_Services.Interfaces;

namespace Music_Review_Application_GUI.Pages.Forms
{
    public class AlbumPageModel : PageModel
    {
        private readonly IAlbumDbManager _albumDbManager;
        private readonly IAlbumService _albumService;

        public Album Album { get; set; }
        public List<Genre> Genres { get; set; }
        public List<AlbumReview> WrittenReviews { get; set; }
        public static int Ratings { get; private set; }

        public AlbumPageModel(IAlbumDbManager albumDbManager, IAlbumService albumService)
        {
            _albumDbManager = albumDbManager;
            _albumService = albumService;
        }

        public IActionResult OnGet(int albumId)
        {
            Album = _albumDbManager.GetAlbum(albumId);
            var allReviews = _albumDbManager.GetAlbumReviews(albumId);
            Ratings = allReviews.Where(r => r.Score != 0).ToList().Count;
            WrittenReviews = allReviews
                .Where(r => !string.IsNullOrEmpty(r.Review))
                .ToList();

            if (Album == null)
            {
                return RedirectToPage("/Forms/NotFound");
            }

            Album.Score = _albumDbManager.GetScore(albumId);
            Genres = _albumService.GetAlbumGenres(Album);
            return Page();
        }
        public IActionResult OnPost()
        {
            return Page();
        }
    }
}
