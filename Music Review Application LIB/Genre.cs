using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music_Review_Application_LIB
{
    class Genre
    {
        #region Properties

        public int Id { get; set; }
        public string Name { get; set; }

        #endregion

        #region Constructors

        public Genre(string name)
        {
            Name = name;
        }

        #endregion

        #region Methods

        public static List<Genre> GetGenres()
        {
            List<Genre> genres = new List<Genre>();

            foreach (Genre genre in genres)
            {

            }

            return genres;
        }

        private async Task GenerateId()
        {

        }


        #endregion
    }
}
