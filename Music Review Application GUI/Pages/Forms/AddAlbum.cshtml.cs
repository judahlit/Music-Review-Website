using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Music_Review_Application_GUI.Models;
using Music_Review_Application_Models;
using Music_Review_Application_Services.Interfaces;

namespace Music_Review_Application_GUI.Pages.Forms
{
    public class AddAlbumModel : PageModel
    {
        private readonly ICreateService _createService;


        [Required, BindProperty]
        public AlbumViewModel Album { get; set; }

        public static string Message { get; set; }

        public AddAlbumModel(ICreateService createService)
        {
            _createService = createService;
        }

        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                var tracks = new List<Track>();
                Message = _createService.CreateAlbum(Album.Title, tracks, Album.ArtistNames, Album.ReleaseDate, Album.ImgPath);
            }
            return Page();
        }
    }
}
