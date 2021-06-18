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
    public class SongRatingPageModel : PageModel
    {
        private readonly ISongDbManager _songDbManager;

        [BindProperty]
        public int Rating { get; set; }
        [BindProperty]
        public string Username { get; set; }
        public static Song Song { get; set; }
        public static List<SongReview> WrittenReviews { get; set; }

        public SongRatingPageModel(ISongDbManager songDbManager)
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

            return Page();
        }
        public IActionResult OnPost()
        {
            if (Rating == 0) return Page();

            var oldReviewId = _songDbManager.GetReviewId(Song.Id, Username);
            if (oldReviewId == 0)
            {
                var songReview = new SongReview(Song.Id, Username, Rating, "");
                _songDbManager.AddReview(songReview);
            }
            else
            {
                var oldReview = _songDbManager.GetSongReview(oldReviewId);
                var newReview = new SongReview(Song.Id, Username, Rating, oldReview.Review);
                _songDbManager.UpdateReview(newReview);
            }

            return RedirectToPage($"/Forms/SongPage", new { songId = Song.Id });
        }
    }
}
