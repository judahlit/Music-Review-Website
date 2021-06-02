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

        int GetSongId(string title, List<string> artistNames);

        SingleSong GetSingle(int id);

        Track GetTrack(int id);

        /*
        List<Song> GetSongs();

        List<Song> GetSong(string title, bool byDate, bool byScore, List<string> genres);

        SongScore GetUserScore(Song song, string username);

        void UpdateScore(int songId, SongScore userScore);
        */

        void DeleteSingle(int id);

        void DeleteTrack(int id);

        bool SongExistsInDb(Song song);

        bool SingleIsAdded(SingleSong single);

        bool TrackIsAdded(SingleSong single);
    }
}
