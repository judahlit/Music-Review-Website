using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music_Review_Application_LIB.DbManagers
{
    public class SongDbManager
    {
        #region Constants and Fields

        private const string QueryAddSong = "INSERT INTO song(title, dateOfRelease, trackId, score, albumId) VALUES('{0}','{1}',{2},{3},{4});";
        private const string QueryAddSongArtist = "INSERT INTO songArtist(songId, artistId) VALUES({0},{1});";
        private const string QueryAddSongGenre = "INSERT INTO songGenre(songId, genre) VALUES({0},'{1}');";
        private const string QueryGetSongId = "SELECT id FROM song WHERE title = '{0}' AND dateOfRelease = '{1}';";
        private const string QueryGetSongById = "SELECT * FROM song WHERE id = {0};";
        private const string QueryGetSongByTitleAndDate = "";
        private const string QueryGetAllSongs = "";
        private const string QueryGetSortedSongs = "";
        private const string QueryUpdateScore = "";
        private const string QueryGetUserScore = "";
        private const string QueryDeleteSong = "";

        #endregion

        public SongDbManager()
        {

        }

        #region Methods

        public void AddSingle(Song song)
        {
            using (SqlConnection conn = new SqlConnection(AppManager.ConnectionString))
            {
                using (SqlCommand query = new SqlCommand(QueryAddSong))
                {
                    conn.Open();


                }
            }
        }

        public void AddPartOfAlbumSong(Song song)
        {

        }
        /*
        public int GetSongId(string title, DateTime dateOfRelease)
        {
            
        }

        public Song GetSong(int id)
        {
            
        }

        public Song GetSong(string title, DateTime dateOfRelease)
        {

        }

        public List<Song> GetSongs()
        {
            
        }

        public List<Song> GetSong(string title, bool byDate, bool byScore, List<string> genres)
        {

        }

        public SongScore GetUserScore(Song song, string username)
        {

        }

        public void UpdateScore(int songId, SongScore userScore)
        {

        }

        public void DeleteSong(int id)
        {

        }
        */

        #endregion
    }
}
