using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music_Review_Application_Models
{
    public class SongReview
    {
        #region Properties

        public int Id { get; set; }

        public int SongId { get; set; }

        public string Username { get; set; }

        public float Score { get; set; }

        public string Review { get; set; }

        #endregion

        public SongReview(int songId, string username, float score, string review)
        {
            SongId = songId;
            Username = username;
            Score = score;
            Review = review;
        }
    }
}
