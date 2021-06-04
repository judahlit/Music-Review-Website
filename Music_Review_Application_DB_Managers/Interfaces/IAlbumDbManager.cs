using System.Collections.Generic;
using Music_Review_Application_Models;

namespace Music_Review_Application_DB_Managers.Interfaces
{
    public interface IAlbumDbManager
    {
        void AddAlbum(Album album);

        void AddReview(AlbumReview albumReview);

        int GetAlbumId(string title, List<string> artistNames);

        int GetReviewId(int albumId, string username);

        Album GetAlbum(int id);

        AlbumReview GetAlbumReview(int id);

        /*
        List<Album> GetAlbums();

        List<Album> GetAlbums(string title, bool byDate, bool byScore, List<string> genres);
        */

        void UpdateReview(AlbumReview albumReview);

        void DeleteAlbum(int id);

        void DeleteReview(int id);

        bool AlbumExistsInDb(Album album);

        bool AlbumIsAdded(Album album);
    }
}
