using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music_Review_Application_Models
{
    public class UserList
    {
        public string Username { get; set; }

        public List<SongReview> SongReviews { get; set; }

        public List<AlbumReview> AlbumReviews { get; set; }

        public UserList(string username, List<SongReview> songReviews, List<AlbumReview> albumReviews)
        {
            Username = username;
            SongReviews = songReviews;
            AlbumReviews = albumReviews;
        }
    }
}
