using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Music_Review_Application_Models;

namespace Music_Review_Application_DB_Managers.Interfaces
{
    public interface ISongDbManager
    {
        void AddSingle(SingleSong single);

        void AddTrack(Track track, int albumId);

        void AddReview(SongReview songReview);

        int GetSongId(string title, List<string> artistNames);

        double GetScore(int songId);

        int GetReviewId(int songId, string username);

        SingleSong GetSingle(int id);

        Track GetTrack(int id);

        Song GetSong(int id);

        SongReview GetSongReview(int id);

        List<Song> GetSongs();

        List<Song> GetSongsWithGenre(int genreId);

        List<Song> GetArtistSongs(int artistId);

        List<SongReview> GetSongReviews(int songId);

        void UpdateReview(SongReview songReview);

        void DeleteSong(int id);

        void DeleteReview(int id);

        bool SongExistsInDb(Song song);

        bool SingleIsAdded(SingleSong single);

        bool TrackIsAdded(Track track, int albumId);
    }
}
