using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Music_Review_Application_Models;

namespace Music_Review_Application_DB_Managers.Interfaces
{
    public interface IGenreDbManager
    {
        void AddGenre(Genre genre);

        void AddGenre(string genreName);

        Genre GetGenre(int genreId);

        Genre GetGenre(string genreName);

        List<Genre> GetGenres();

        int GetGenreId(string genreName);

        void CheckGenres(List<Genre> genres);
    }
}
