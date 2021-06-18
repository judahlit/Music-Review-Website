using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Music_Review_Application_DB_Managers;
using Music_Review_Application_DB_Managers.Interfaces;
using Music_Review_Application_GUI.Models;
using Music_Review_Application_Models;
using Music_Review_Application_Services.Interfaces;

namespace Music_Review_Application_GUI.Pages.Forms
{
    public class AddSongModel : PageModel
    {
        private readonly ICreateService _createService;


        [BindProperty]
        public Models.SongViewModel Song { get; set; }
        public static string Message { get; set; }

        public AddSongModel(ICreateService createService)
        {
            _createService = createService;
        }

        public void OnGet()
        {
            
        }

        public IActionResult OnPost()
        {
            Message = _createService.CreateSingle(Song.Title, Song.ArtistNames, Song.GenreNames, Song.DateDay, Song.DateMonth, Song.DateYear, Song.ImgPath);
            return Page();
        }
    }
}
