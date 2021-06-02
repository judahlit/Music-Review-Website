using System.Collections.Generic;
using Music_Review_Application_Models;

namespace Music_Review_Application_DB_Managers.Interfaces
{
    public interface IAlbumDbManager
    {
        void AddAlbum(Album album);

        int GetAlbumId(string title, List<string> artistNames);

        Album GetAlbum(int id);

        /*
        List<Album> GetAlbums();

        List<Album> GetAlbums(string title, bool byDate, bool byScore, List<string> genres);

        AlbumScore GetAlbumScore(Album album, string username);

        void UpdateScore(int albumId, AlbumScore userScore);
        */

        void DeleteAlbum(int id);

        bool AlbumExistsInDb(Album album);

        bool AlbumIsAdded(Album album);
    }
}
