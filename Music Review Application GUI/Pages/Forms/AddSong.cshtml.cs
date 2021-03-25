using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Music_Review_Application_GUI.Pages.Forms
{
    public class AddSongModel : PageModel
    {
        [BindProperty]
        public AddSongModel Song { get; set; }

        public void OnGet()
        {
        }

        public void OnPost()
        {

        }
    }
}
