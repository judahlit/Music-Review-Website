using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Music_Review_Application_GUI.Models
{
    public class AlbumReviewViewModel
    {
        public int AlbumId { get; }

        public AlbumViewModel Album { get; set; }

        public string Username { get; }

        public float Score { get; }

        public string Review { get; }
    }
}
