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

        public double songScore { get; set; }

        public string songReview { get; set; }

        #endregion
    }
}
