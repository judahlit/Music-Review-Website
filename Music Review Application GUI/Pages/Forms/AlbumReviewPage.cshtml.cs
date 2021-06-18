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
    public class AlbumReviewPageModel : PageModel
    {
        private readonly IAlbumDbManager _albumDbManager;
        private readonly IAlbumService _albumService;

        public static Album Album { get; set; }

        [BindProperty]
        public List<Genre> Genres { get; set; } = new();
        public List<AlbumReview> WrittenReviews { get; set; } = new();
        public static string Message { get; set; }
        [BindProperty]
        public string Username { get; set; }
        [BindProperty]
        public string Review { get; set; }

        public AlbumReviewPageModel(IAlbumDbManager albumDbManager, IAlbumService albumService)
        {
            _albumDbManager = albumDbManager;
            _albumService = albumService;
        }

        public IActionResult OnGet(int albumId)
        {
            Album = _albumDbManager.GetAlbum(albumId);
            WrittenReviews = _albumDbManager.GetAlbumReviews(albumId)
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
            if (string.IsNullOrEmpty(Review)) return Page();

            var oldReviewId = _albumDbManager.GetReviewId(Album.Id, Username);
            if (oldReviewId == 0)
            {
                Message = "You can't review an album you haven't rated yet.";
                return Page();
            }
            else
            {
                var oldReview = _albumDbManager.GetAlbumReview(oldReviewId);
                var newReview = new AlbumReview(Album.Id, Username, oldReview.Score, Review);
                _albumDbManager.UpdateReview(newReview);
            }

            return RedirectToPage($"/Forms/AlbumPage", new { albumId = Album.Id });
        }
    }
}
