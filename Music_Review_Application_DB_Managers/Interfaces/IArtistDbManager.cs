using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Music_Review_Application_Models;

namespace Music_Review_Application_DB_Managers.Interfaces
{
    public interface IArtistDbManager
    {
        void AddArtist(Artist artist);

        int GetArtistId(string artistName);

        Artist GetArtist(int id);

        /*
        List<Artist> GetArtists();
        */

        void CheckArtists(List<string> artistNames);
    }
}
