using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Music_Review_Application_GUI.Models;

namespace Music_Review_Application_GUI.Pages.Forms
{
    public class AddSongModel : PageModel
    {
        [BindProperty]
        public SongModel Song { get; set; }

        public static string ErrorMessage { get; set; }

        public void OnGet()
        {
            if (ErrorMessage != null)
            {
                // TODO: Show error message
                ErrorMessage = null;
            }
        }

        public IActionResult OnPost()
        {
            if (ErrorMessage == null)
            {
                return RedirectToPage("/Index");
            }

            return Page();
        }
    }
}
