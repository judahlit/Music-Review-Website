using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Music_Review_Application_GUI.Models
{
    public class AlbumReviewViewModel
    {
        public int AlbumId { get; set; }

        public string Username { get; set; }

        public float Score { get; set; }

        public string Review { get; set; }
    }
}
