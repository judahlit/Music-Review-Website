using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Music_Review_Application_DB_Managers.Interfaces;
using Music_Review_Application_Models;
using Music_Review_Application_Sample_Data;

namespace Music_Review_Application_GUI.Pages.Forms
{
    public class DiscoverModel : PageModel
    {
        private readonly ISongDbManager _songDbManager;
        private readonly IAlbumDbManager _albumDbManager;
        private readonly IGenreDbManager _genreDbManager;

        public static List<Song> Songs { get; set; } = new();
        public static List<Album> Albums { get; set; } = new();
        public static List<Genre> Genres { get; set; }

        public DiscoverModel(ISongDbManager songDbManager, IAlbumDbManager albumDbManager, IGenreDbManager genreDbManager)
        {
            _songDbManager = songDbManager;
            _albumDbManager = albumDbManager;
            _genreDbManager = genreDbManager;
        }

        public void OnGet()
        {
            var receivedSongs = _songDbManager.GetSongs();
            var receivedAlbums = _albumDbManager.GetAlbums();
            var receivedGenres = _genreDbManager.GetGenres();
            //var receivedSongs = SampleData.GetSampleAlbum().Tracks;
            //var receivedAlbums = new List<Album>{ SampleData.GetSampleAlbum() };

            foreach (var song in receivedSongs)
            {
                var titles = Songs.Select(s => s.Title).ToList();
                if (!titles.Contains(song.Title)) Songs.Add(song);
            }

            foreach (var album in receivedAlbums)
            {
                var titles = Albums.Select(a => a.Title).ToList();
                if (!titles.Contains(album.Title)) Albums.Add(album);
            }

            foreach (var genre in receivedGenres)
            {
                var genreNames = Genres.Select(g => g.GenreName).ToList();
                if (!genreNames.Contains(genre.GenreName)) Genres.Add(genre);
            }
        }

        public void OnGet(int genreId)
        {
            var receivedSongs = _songDbManager.GetSongsWithGenre(genreId);
            //var receivedAlbums = _albumDbManager.GetAlbumsWithGenre(genreId);
            var receivedGenres = _genreDbManager.GetGenres();
            //var receivedSongs = SampleData.GetSampleAlbum().Tracks;
            //var receivedAlbums = new List<Album>{ SampleData.GetSampleAlbum() };

            foreach (var song in receivedSongs)
            {
                var titles = Songs.Select(s => s.Title).ToList();
                if (!titles.Contains(song.Title)) Songs.Add(song);
            }
            /*
            foreach (var album in receivedAlbums)
            {
                var titles = Albums.Select(a => a.Title).ToList();
                if (!titles.Contains(album.Title)) Albums.Add(album);
            }
            */
            foreach (var genre in receivedGenres)
            {
                var genreNames = Genres.Select(g => g.GenreName).ToList();
                if (!genreNames.Contains(genre.GenreName)) Genres.Add(genre);
            }
        }
    }
}
