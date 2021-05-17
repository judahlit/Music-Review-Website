using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Music_Review_Application_GUI.Models;
using Music_Review_Application_LIB;
using Music_Review_Application_LIB.DbManagers;
using SingleSong = Music_Review_Application_LIB.SingleSong;

namespace Music_Review_Application_GUI.Pages.Forms
{
    public class AddSongModel : PageModel
    {

        [BindProperty]
        public SongModel Song { get; set; }
        public static string ErrorMessage { get; set; }

        public void OnGet()
        {
            
        }

        public IActionResult OnPost()
        {
            ErrorMessage = null;

            if (ToDateTime())
            {
                AddSingleToDB();
            }
            else
            {
                ErrorMessage = "Please fill in a valid date in the 'Date of Release' field.";
            }

            if (ErrorMessage != null)
            {
                return Page();
            }

            return RedirectToPage("/Index");
        }

        public void AddSingleToDB()
        {
            SingleSong single = new(Song.Title, Song.Date, null, Song.ArtistNames, Song.GenreNames);
            SongDbManager songDbManager = new();

            if (songDbManager.GetSongId(single.Title, single.DateOfRelease) == 0)
            {
                songDbManager.AddSingle(single);
            }
        }

        public bool ToDateTime()
        {
            DateTime correctDate;

            if (DateTime.TryParse($"{Song.DateYear}-{Song.DateMonth}-{Song.DateDay}", out correctDate) && DateTime.Compare(correctDate, DateTime.Now) <= 0)
            {
                Song.Date = correctDate;
                return true;
            }

            return false;
        }
    }
}
