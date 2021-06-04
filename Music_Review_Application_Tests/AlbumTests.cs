using System;
using System.Collections.Generic;
using Xunit;
using System.Drawing;
using Autofac;
using Moq;
using Music_Review_Application_DB_Managers;
using Music_Review_Application_DB_Managers.Interfaces;
using Music_Review_Application_Models;

namespace Music_Review_Application_Tests
{
    public class AlbumTests
    {
        [Fact] public void GetsAlbumIdBySearchingAlbumTitleAndArtists()
        {
            var album = GetSampleAlbum();
            var albumId = 0;
            var container = TestContainerConfig.Configure();

            using (var scope = container.BeginLifetimeScope())
            {
                var albumDbManager = scope.Resolve<IAlbumDbManager>();
                albumDbManager.AddAlbum(album);
                albumId = albumDbManager.GetAlbumId(album.Title, album.ArtistNames);
                albumDbManager.DeleteAlbum(albumDbManager.GetAlbumId(album.Title, album.ArtistNames));
            }

            Assert.True(albumId > 0);
        }

        [Fact]
        public void AlbumGetsAddedToDB()
        {
            var albumAdded = false;
            var album = GetSampleAlbum();
            var container = TestContainerConfig.Configure();

            using (var scope = container.BeginLifetimeScope())
            {
                var albumDbManager = scope.Resolve<IAlbumDbManager>();
                albumAdded = albumDbManager.AlbumIsAdded(album);
                albumDbManager.DeleteAlbum(albumDbManager.GetAlbumId(album.Title, album.ArtistNames));
            }

            Assert.True(albumAdded);
        }

        private Album GetSampleAlbum()
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
            return new("Somewhere Not in This World", tracks, new DateTime(2020, 04, 20), img, albumArtists);
        }
    }
}
