using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music_Review_Application_Models
{
    public class Album
    {
        #region Properties

        public int Id { get; set; }

        public string Title { get; set; }

        public List<Track> Tracks { get; set; }

        public DateTime DateOfRelease { get; set; }

        public double Score { get; set; }

        public Image Img { get; set; }

        public List<string> ArtistNames { get; set; }

        #endregion

        public Album(string title, List<Track> tracks, DateTime dateOfRelease, Image img, List<string> artistNames)
        {
            Title = title;
            Tracks = tracks;
            DateOfRelease = dateOfRelease;
            Img = img;
            ArtistNames = artistNames;
        }

        public List<Genre> GetAlbumGenres()
        {
            List<Genre> genres = new();
            List<string> genreNames = new();

            foreach (Track track in Tracks)
            {
                foreach (var genre in track.Genres.Where(genre => !genreNames.Contains(genre.GenreName)))
                {
                    genres.Add(genre);
                    genreNames.Add(genre.GenreName);
                }
            }

            return genres;
        }
    }
}
