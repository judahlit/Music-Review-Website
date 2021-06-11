using System.Collections.Generic;
using Autofac;
using Music_Review_Application_DB_Managers.Interfaces;
using Music_Review_Application_Models;
using Music_Review_Application_Sample_Data;
using Xunit;

namespace Music_Review_Application_Integration_Tests
{
    [Collection("Sequential")]
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

                // Review and album get added to the db
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

                // Review and album added to the db
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

                // Review and album get added to the db
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
            // Arrange
            var scores = new List<int>();

            var username = "User123";
            var album = SampleData.GetSampleAlbum();
            var container = TestContainerConfig.Configure();

            using (var scope = container.BeginLifetimeScope())
            {
                var albumDbManager = scope.Resolve<IAlbumDbManager>();
                var songDbManager = scope.Resolve<ISongDbManager>();
                var userListDbManager = scope.Resolve<IUserListDbManager>();

                albumDbManager.AddAlbum(album);

                // Reviews and album get added to the db
                songDbManager.AddReview(new SongReview(songDbManager.GetSongId(album.Tracks[0].Title, album.Tracks[0].ArtistNames), username, 6, ""));
                songDbManager.AddReview(new SongReview(songDbManager.GetSongId(album.Tracks[1].Title, album.Tracks[1].ArtistNames), username, 8, ""));
                albumDbManager.AddReview(new AlbumReview(albumDbManager.GetAlbumId(album.Title, album.ArtistNames), username, 7, ""));

                var userList = userListDbManager.GetUserList(username);

                // Act
                scores.Add(userList.SongReviews[0].Score);
                scores.Add(userList.SongReviews[1].Score);
                scores.Add(userList.AlbumReviews[0].Score);

                // Reviews and album get deleted from the db
                songDbManager.DeleteReview(songDbManager.GetReviewId(songDbManager.GetSongId(album.Tracks[0].Title, album.Tracks[0].ArtistNames), username));
                songDbManager.DeleteReview(songDbManager.GetReviewId(songDbManager.GetSongId(album.Tracks[1].Title, album.Tracks[1].ArtistNames), username));
                albumDbManager.DeleteReview(albumDbManager.GetReviewId(albumDbManager.GetAlbumId(album.Title, album.ArtistNames), username));
                albumDbManager.DeleteAlbum(albumDbManager.GetAlbumId(album.Title, album.ArtistNames));
            }

            // Assert
            Assert.Equal(new List<int>{6, 8, 7}, scores);
        }
    }
}
