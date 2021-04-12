using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music_Review_Application_LIB
{
    public class Song
    {
        #region Enums

        public enum SongType
        {
            Single,
            TrackOnAlbum
        }

        #endregion

        #region Constants and Fields

        private const string _connectionString = "";
        private const string _queryAddSingleToDB = "";
        private const string _queryAddPartOfAlbumSongToDB = "";
        private const string _queryGetSongId = "";
        private const string _queryGetSong = "";

        #endregion


        #region Properties

        public int Id { get; private set; }

        public string Title { get; private set; }

        public DateTime DateOfRelease { get; private set; }

        public int TrackId { get; private set; }
        
        public int AlbumId { get; private set; }

        public string Img { get; private set; }

        public List<Artist> Artists { get; private set; } = new List<Artist>();

        public List<string> GenreNames { get; private set; } = new List<string>();

        #endregion


        #region Constructors

        public Song(string title, DateTime date, int trackId, int albumId, List<Artist> artists, List<string> genreNames)
        {
            Title = title;
            DateOfRelease = date;
            TrackId = trackId;
            AlbumId = albumId;
            Artists = artists;
            GenreNames = genreNames;

            AddSingleToDB();
        }

        public Song(string title, DateTime date, string img, List<Artist> artists, List<string> genreNames)
        {
            Title = title;
            DateOfRelease = date;
            Img = img;
            Artists = artists;
            GenreNames = genreNames;

            AddPartOfAlbumSongToDB();
        }

        #endregion


        #region Methods

        private async Task AddSingleToDB()
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                using (SqlCommand query = new SqlCommand(_queryAddSingleToDB))
                {
                    conn.Open();
                    

                }
            }

            await GetSongId(Title, Artists);
        }

        private async Task AddPartOfAlbumSongToDB()
        {


            await GetSongId(Title, Artists);
        }

        public async Task<int> GetSongId(string title, List<Artist> artists)
        {
            return -1;
        }

        public async Task<Song> GetSong(int id)
        {
            return null;
        }

        public async Task<List<Song>> GetAllSongs()
        {

            throw new NotImplementedException();
        }

        public async Task<double> GetScore()
        {
            double avgScore = 0;

            return avgScore;
        }



        #endregion
    }
}
