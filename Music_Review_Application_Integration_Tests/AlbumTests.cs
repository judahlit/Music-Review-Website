using System.Collections.Generic;
using Xunit;
using Autofac;
using Music_Review_Application_DB_Managers.Interfaces;
using Music_Review_Application_Sample_Data;

namespace Music_Review_Application_Integration_Tests
{
    [Collection("Sequential")]
    public class AlbumTests
    {
        [Fact]
        public void GetsAlbumIdBySearchingAlbumTitleAndArtists()
        {
            var album = SampleData.GetSampleAlbum();

            // Arrange
            var albumId = 0;

            var container = TestContainerConfig.Configure();
            using (var scope = container.BeginLifetimeScope())
            {
                var albumDbManager = scope.Resolve<IAlbumDbManager>();
                albumDbManager.AddAlbum(album);

                // Act
                albumId = albumDbManager.GetAlbumId(album.Title, album.ArtistNames);

                albumDbManager.DeleteAlbum(albumDbManager.GetAlbumId(album.Title, album.ArtistNames));
            }

            // Assert
            Assert.True(albumId > 0);
        }
        
        [Fact]
        public void GetSongIdReturnsZeroWhenSearchingNonExistentArtists()
        {
            var album = SampleData.GetSampleAlbum();

            // Arrange
            var albumId = 0;
            var nonExistingArtists = new List<string> { "jsoiapfjdiosjiop", "doafjdsopj" };

            var container = TestContainerConfig.Configure();
            using (var scope = container.BeginLifetimeScope())
            {
                var albumDbManager = scope.Resolve<IAlbumDbManager>();
                albumDbManager.AddAlbum(album);
                var album2 = SampleData.GetSampleAlbum();
                album2.ArtistNames = nonExistingArtists;

                // Act
                albumId = albumDbManager.GetAlbumId(album2.Title, album2.ArtistNames);

                albumDbManager.DeleteAlbum(albumDbManager.GetAlbumId(album.Title, album.ArtistNames));
            }

            // Assert
            Assert.Equal(0, albumId);
        }
        
        [Fact]
        public void AlbumGetsAddedToDB()
        {
            // Arrange
            var albumAdded = false;

            var album = SampleData.GetSampleAlbum();
            var container = TestContainerConfig.Configure();
            using (var scope = container.BeginLifetimeScope())
            {
                var albumDbManager = scope.Resolve<IAlbumDbManager>();

                // Act
                albumAdded = albumDbManager.AlbumIsAdded(album);

                albumDbManager.DeleteAlbum(albumDbManager.GetAlbumId(album.Title, album.ArtistNames));
            }

            // Assert
            Assert.True(albumAdded);
        }

        [Fact]
        public void ReturnsTheScoreOfAnAlbum()
        {
            // Arrange
            var score = 0.0;

            var album = SampleData.GetSampleAlbum();
            var container = TestContainerConfig.Configure();
            using (var scope = container.BeginLifetimeScope())
            {
                var albumDbManager = scope.Resolve<IAlbumDbManager>();
                albumDbManager.AddAlbum(album);
                var albumId = albumDbManager.GetAlbumId(album.Title, album.ArtistNames);
                var reviews = SampleData.GetSampleAlbumReviews(albumId);
                albumDbManager.AddReview(reviews[0]);
                albumDbManager.AddReview(reviews[1]);
                albumDbManager.AddReview(reviews[2]);

                // Act
                score = albumDbManager.GetScore(albumId);

                albumDbManager.DeleteAlbum(albumDbManager.GetAlbumId(album.Title, album.ArtistNames));
            }

            Assert.Equal(8, score);
        }
    }
}
