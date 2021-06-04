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
            // Arrange
            var artistId = 0;

            var container = TestContainerConfig.Configure();
            using (var scope = container.BeginLifetimeScope())
            {
                var artistDbManager = scope.Resolve<IArtistDbManager>();

                // Act
                artistId = artistDbManager.GetArtistId("Taishi");
            }

            // Assert
            Assert.True(artistId > 0);
        }

        [Fact]
        public void ReturnsArtist()
        {
            // Arrange
            var artistName = "";

            var container = TestContainerConfig.Configure();
            using (var scope = container.BeginLifetimeScope())
            {
                var artistDbManager = scope.Resolve<IArtistDbManager>();

                // Act
                artistName = artistDbManager.GetArtist(artistDbManager.GetArtistId("Taishi")).ArtistName;
            }

            // Assert
            Assert.Equal("Taishi", artistName);
        }
    }
}
