using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Music_Review_Application_GUI.Models;
using Music_Review_Application_LIB;

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
            Song song = new Song(Song.Title, Song.Artist, 0, Song.Date);
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
