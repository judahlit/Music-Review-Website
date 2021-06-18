using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Music_Review_Application_DB_Managers.Interfaces;
using Music_Review_Application_GUI.Models;
using Music_Review_Application_Models;

namespace Music_Review_Application_GUI.Pages
{
    public class ArtistPageModel : PageModel
    {
        private readonly IArtistDbManager _artistDbManager;
        private readonly ISongDbManager _songDbManager;
        private readonly IAlbumDbManager _albumDbManager;

        public Artist Artist { get; set; }

        public ArtistPageModel(IArtistDbManager artistDbManager, ISongDbManager songDbManager, IAlbumDbManager albumDbManager)
        {
            _artistDbManager = artistDbManager;
            _songDbManager = songDbManager;
            _albumDbManager = albumDbManager;
        }

        public IActionResult OnGet(string artistName)
        {
            var artistId = _artistDbManager.GetArtistId(artistName);
            Artist = _artistDbManager.GetArtist(artistId);

            if (Artist == null)
            {
                return RedirectToPage("/Forms/NotFound");
            }

            Artist.Songs = _songDbManager.GetArtistSongs(artistId);
            Artist.Albums = _albumDbManager.GetArtistAlbums(artistId);

            return Page();
        }
    }
}
