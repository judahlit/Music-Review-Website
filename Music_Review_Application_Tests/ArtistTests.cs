using System;
using System.Collections.Generic;
using Autofac;
using Moq;
using Music_Review_Application_DB_Managers;
using Music_Review_Application_DB_Managers.Interfaces;
using Music_Review_Application_Models;
using Xunit;

namespace Music_Review_Application_Tests
{
    public class ArtistTests
    {
        [Fact]
        public void ReturnsArtistId()
        {
            var artistId = 0;
            var container = TestContainerConfig.Configure();

            using (var scope = container.BeginLifetimeScope())
            {
                var artistDbManager = scope.Resolve<IArtistDbManager>();
                artistId = artistDbManager.GetArtistId("Taishi");
            }

            Assert.True(artistId > 0);
        }

        [Fact]
        public void ReturnsArtist()
        {
            var artistName = "";
            var container = TestContainerConfig.Configure();

            using (var scope = container.BeginLifetimeScope())
            {
                var artistDbManager = scope.Resolve<IArtistDbManager>();
                artistName = artistDbManager.GetArtist(artistDbManager.GetArtistId("Taishi")).ArtistName;
            }

            Assert.Equal("Taishi", artistName);
        }
    }
}
