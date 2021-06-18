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
    public class SongPageModel : PageModel
    {
        private readonly ISongDbManager _songDbManager;
        public static Song Song { get; set; }
        public static int Ratings { get; private set; }
        public static List<SongReview> WrittenReviews { get; set; }

        public SongPageModel(ISongDbManager songDbManager)
        {
            _songDbManager = songDbManager;
        }

        public IActionResult OnGet(int songId)
        {
            Song = _songDbManager.GetSong(songId);
            var allReviews = _songDbManager.GetSongReviews(songId);
            Ratings = allReviews.Where(r => r.Score != 0).ToList().Count;
            WrittenReviews = allReviews
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
            return Page();
        }
    }
}
