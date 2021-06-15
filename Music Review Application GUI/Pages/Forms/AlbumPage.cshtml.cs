using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Music_Review_Application_DB_Managers.Interfaces;

namespace Music_Review_Application_GUI.Pages.Forms
{
    public class AlbumPageModel : PageModel
    {
        private readonly IAlbumDbManager _albumDbManager;
        public static int AlbumId { get; set; }

        public AlbumPageModel(IAlbumDbManager albumDbManager)
        {
            _albumDbManager = albumDbManager;
        }

        public void OnGet()
        {
        }
    }
}
