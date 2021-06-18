using Autofac;
using Music_Review_Application_DB_Managers.Interfaces;
using Music_Review_Application_Sample_Data;
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

        [Fact]
        public void ReturnsArtistSongs()
        {
            // Arrange
            var songCount = 0;

            var sampleAlbum = SampleData.GetSampleAlbum();

            var container = TestContainerConfig.Configure();
            using (var scope = container.BeginLifetimeScope())
            {
                var artistDbManager = scope.Resolve<IArtistDbManager>();
                var albumDbManager = scope.Resolve<IAlbumDbManager>();
                var songDbManager = scope.Resolve<ISongDbManager>();

                albumDbManager.AddAlbum(sampleAlbum);
                var artist = artistDbManager.GetArtist(artistDbManager.GetArtistId("Taishi"));
                artist.Songs = songDbManager.GetArtistSongs(artist.Id);

                // Act
                songCount = artist.Songs.Count;

                albumDbManager.DeleteAlbum(albumDbManager.GetAlbumId(sampleAlbum.Title, sampleAlbum.ArtistNames));
            }

            // Assert
            Assert.Equal(5, songCount);
        }

        [Fact]
        public void ReturnsArtistAlbums()
        {
            // Arrange
            var albumCount = 0;

            var sampleAlbum = SampleData.GetSampleAlbum();

            var container = TestContainerConfig.Configure();
            using (var scope = container.BeginLifetimeScope())
            {
                var artistDbManager = scope.Resolve<IArtistDbManager>();
                var albumDbManager = scope.Resolve<IAlbumDbManager>();

                albumDbManager.AddAlbum(sampleAlbum);
                var artist = artistDbManager.GetArtist(artistDbManager.GetArtistId("Taishi"));
                artist.Albums = albumDbManager.GetArtistAlbums(artist.Id);

                // Act
                albumCount = artist.Albums.Count;

                albumDbManager.DeleteAlbum(albumDbManager.GetAlbumId(sampleAlbum.Title, sampleAlbum.ArtistNames));
            }

            // Assert
            Assert.Equal(1, albumCount);
        }
    }
}
