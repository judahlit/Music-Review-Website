using Autofac;
using Music_Review_Application_DB_Managers.Interfaces;
using Xunit;

namespace Music_Review_Application_Integration_Tests
{
    [Collection("Sequential")]
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
