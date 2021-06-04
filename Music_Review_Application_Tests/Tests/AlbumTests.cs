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
            var album = SampleData.GetSampleAlbum();
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
            var album = SampleData.GetSampleAlbum();
            var container = TestContainerConfig.Configure();

            using (var scope = container.BeginLifetimeScope())
            {
                var albumDbManager = scope.Resolve<IAlbumDbManager>();
                albumAdded = albumDbManager.AlbumIsAdded(album);
                albumDbManager.DeleteAlbum(albumDbManager.GetAlbumId(album.Title, album.ArtistNames));
            }

            Assert.True(albumAdded);
        }
    }
}
