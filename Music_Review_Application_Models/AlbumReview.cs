using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music_Review_Application_Models
{
    public class AlbumReview
    {
        public int Id { get; set; }

        public int AlbumId { get; set; }

        public string Username { get; set; }

        public int Score { get; set; }

        public string Review { get; set; }


        public AlbumReview(int albumId, string username, int score, string review)
        {
            AlbumId = albumId;
            Username = username;
            Score = score;
            Review = review;
        }
    }
}
