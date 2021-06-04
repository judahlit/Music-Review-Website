using System;
using Autofac;
using Music_Review_Application_DB_Managers.Interfaces;
using Music_Review_Application_Models;
using Xunit;

namespace Music_Review_Application_Tests
{
    public class ScoreTests
    {
        [Fact]
        public void UserGivesASongAReview()
        {
            // Arrange
            var reviewId = 0;
            var reviewScore = 0;

            var album = SampleData.GetSampleAlbum();
            var song = album.Tracks[1];
            var container = TestContainerConfig.Configure();

            using (var scope = container.BeginLifetimeScope())
            {
                var songDbManager = scope.Resolve<ISongDbManager>();
                var albumDbManager = scope.Resolve<IAlbumDbManager>();

                // Review gets added to the db
                albumDbManager.AddAlbum(album);
                var review = new SongReview(songDbManager.GetSongId(song.Title, song.ArtistNames), "User123", 7, "");
                songDbManager.AddReview(review);

                var returnedReview = songDbManager.GetSongReview(songDbManager.GetReviewId(review.SongId, review.Username));

                // Act
                reviewId = returnedReview.Id;
                reviewScore = returnedReview.Score;

                // Album and review get deleted in the db
                songDbManager.DeleteReview(returnedReview.Id);
                albumDbManager.DeleteAlbum(albumDbManager.GetAlbumId(album.Title, album.ArtistNames));
            }

            // Assert
            Assert.True(reviewId > 0);
            Assert.Equal(7, reviewScore);
        }
        [Fact]
        public void UserUpdatesAGivenSongReview()
        {
            // Arrange
            var reviewScore = 0;
            var reviewSongReview = "";

            var album = SampleData.GetSampleAlbum();
            var song = album.Tracks[1];
            var container = TestContainerConfig.Configure();

            using (var scope = container.BeginLifetimeScope())
            {
                var songDbManager = scope.Resolve<ISongDbManager>();
                var albumDbManager = scope.Resolve<IAlbumDbManager>();

                // Review gets added to the db
                albumDbManager.AddAlbum(album);
                var review = new SongReview(songDbManager.GetSongId(song.Title, song.ArtistNames), "User123", 7, "");
                songDbManager.AddReview(review);

                // Review gets updated and becomes a 'written' review
                review.Score = 8;
                review.Review = "This is an amazing song!!";
                songDbManager.UpdateReview(review);

                var returnedReview = songDbManager.GetSongReview(songDbManager.GetReviewId(review.SongId, review.Username));

                // Act
                reviewScore = returnedReview.Score;
                reviewSongReview = returnedReview.Review;

                // Album and review get deleted in the db
                songDbManager.DeleteReview(returnedReview.Id);
                albumDbManager.DeleteAlbum(albumDbManager.GetAlbumId(album.Title, album.ArtistNames));
            }

            // Assert
            Assert.Equal(8, reviewScore);
            Assert.Equal("This is an amazing song!!", reviewSongReview);
        }

        [Fact]
        public void UserGivesAnAlbumAReview()
        {
            // Arrange
            var reviewId = 0;
            var reviewScore = 0;
            
            var album = SampleData.GetSampleAlbum();
            var container = TestContainerConfig.Configure();

            using (var scope = container.BeginLifetimeScope())
            {
                var albumDbManager = scope.Resolve<IAlbumDbManager>();

                // Review gets added to the db
                albumDbManager.AddAlbum(album);
                var review = new AlbumReview(albumDbManager.GetAlbumId(album.Title, album.ArtistNames), "User123", 8, "");
                albumDbManager.AddReview(review);

                var returnedReview = albumDbManager.GetAlbumReview(albumDbManager.GetReviewId(review.AlbumId, review.Username));

                // Act
                reviewId = returnedReview.Id;
                reviewScore = returnedReview.Score;

                // Album and review get deleted in the db
                albumDbManager.DeleteReview(returnedReview.Id);
                albumDbManager.DeleteAlbum(albumDbManager.GetAlbumId(album.Title, album.ArtistNames));
            }

            // Assert
            Assert.True(reviewId > 0);
            Assert.Equal(8, reviewScore);
        }

        [Fact]
        public void UserGetsAListOfSongsAndAlbumsWhichASpecifiedUserHasReviewed()
        {

        }
    }
}
