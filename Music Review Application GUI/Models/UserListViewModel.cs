using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Music_Review_Application_GUI.Models
{
    public class UserListViewModel
    {
        public string Username { get; set; }

        public List<SongReviewViewModel> SongReviews { get; set; } = new();

        public List<AlbumReviewViewModel> AlbumReviews { get; set; } = new();
    }
}
