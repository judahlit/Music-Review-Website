using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Music_Review_Application_GUI.Models
{
    public class SongReviewViewModel
    {
        public int SongId { get; }

        public SongViewModel Song { get; set; }

        public string Username { get; }

        public float Score { get; }

        public string Review { get; }
    }
}
