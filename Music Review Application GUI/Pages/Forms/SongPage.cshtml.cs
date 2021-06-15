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

        public SongPageModel(ISongDbManager songDbManager)
        {
            _songDbManager = songDbManager;
        }

        public IActionResult OnGet(int songId)
        {
            //Song = _songDbManager.GetSong(songId);
            Song = Music_Review_Application_Sample_Data.SampleData.GetSampleSingle();

            if (Song == null)
            {
                return RedirectToPage("/NotFound");
            }

            return Page();
        }
    }
}
