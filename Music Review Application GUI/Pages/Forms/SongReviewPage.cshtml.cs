using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Music_Review_Application_DB_Managers.Interfaces;
using Music_Review_Application_Models;

namespace Music_Review_Application_GUI.Pages.Forms
{
    public class SongReviewPageModel : PageModel
    {
        private readonly ISongDbManager _songDbManager;
        public static Song Song { get; set; }
        public static List<SongReview> WrittenReviews { get; set; }
        public static string Message { get; set; }
        [BindProperty]
        public string Username { get; set; }
        [BindProperty]
        public string Review { get; set; }

        public SongReviewPageModel(ISongDbManager songDbManager)
        {
            _songDbManager = songDbManager;
        }

        public IActionResult OnGet(int songId)
        {
            Song = _songDbManager.GetSong(songId);
            WrittenReviews = _songDbManager.GetSongReviews(songId)
                .Where(r => !string.IsNullOrEmpty(r.Review))
                .ToList();

            if (Song == null)
            {
                return RedirectToPage("/Forms/NotFound");
            }

            Song.Score = _songDbManager.GetScore(songId);
            return Page();
        }

        public IActionResult OnPost()
        {
            if (string.IsNullOrEmpty(Review)) return Page();

            var oldReviewId = _songDbManager.GetReviewId(Song.Id, Username);
            if (oldReviewId == 0)
            {
                Message = "You can't review an album you haven't rated yet.";
                return Page();
            }
            else
            {
                var oldReview = _songDbManager.GetSongReview(oldReviewId);
                var newReview = new SongReview(Song.Id, Username, oldReview.Score, Review);
                _songDbManager.UpdateReview(newReview);
            }

            return RedirectToPage($"/Forms/SongPage", new { songId = Song.Id });
        }
    }
}
