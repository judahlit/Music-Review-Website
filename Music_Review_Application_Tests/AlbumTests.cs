using System;
using System.Collections.Generic;
using Xunit;
using System.Drawing;
using Music_Review_Application_DB_Managers;
using Music_Review_Application_DB_Managers.Interfaces;
using Music_Review_Application_Models;

namespace Music_Review_Application_Tests
{
    public class AlbumTests
    {
        [Fact]
        public void AlbumGetsAddedToDB()
        {
            Image img = null;
            List<Track> tracks = new();
            List<string> artistNames = new();

            artistNames.Add("Taishi");

            tracks.Add(new Track("Introduction - Somewhere Not in This World", new DateTime(2017, 10, 29), 1, artistNames, new List<Genre> { new("piano"), new("electronic") }));
            tracks.Add(new Track("The Tower Which Is Telling the Time 1", new DateTime(2017, 10, 29), 2, artistNames, new List<Genre> { new("orchestral"), new("electronic") }));
            tracks.Add(new Track("The Tower Which Is Telling the Time 2", new DateTime(2017, 10, 29), 3, artistNames, new List<Genre> { new("orchestral"), new("electronic"), new("EDM") }));
            tracks.Add(new Track("The Tower Which Is Telling the Time 3", new DateTime(2017, 10, 29), 4, artistNames, new List<Genre> { new("orchestral"), new("electronic"), new("EDM"), new("Trance") }));
            tracks.Add(new Track("Encounter Like a Rendezvous (in Another World)", new DateTime(2017, 10, 29), 5, artistNames, new List<Genre> { new("piano") }));

            List<string> albumArtists = new();
            albumArtists.Add("Taishi");

            Album album = new("Somewhere Not in This World", tracks, new DateTime(2020, 04, 20), img, albumArtists);

            IAlbumDbManager _albumDbManager;

            bool albumAdded = _albumDbManager.AlbumIsAdded(album);
            _albumDbManager.DeleteAlbum(_albumDbManager.GetAlbumId(album.Title, album.ArtistNames));

            Assert.True(albumAdded);
        }
    }
}
