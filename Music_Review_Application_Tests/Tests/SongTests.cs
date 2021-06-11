using System.Collections.Generic;
using Autofac;
using Music_Review_Application_DB_Managers.Interfaces;
using Xunit;

namespace Music_Review_Application_Integration_Tests.Tests
{
    [Collection("Sequential")]
    public class SongTests
    {
        [Fact]
        public void GetsSongIdBySearchingSongTitleAndArtists()
        {
            var song = SampleData.GetSampleSingle();

            // Arrange
            var songId = 0;

            var container = TestContainerConfig.Configure();
            using (var scope = container.BeginLifetimeScope())
            {
                var songDbManager = scope.Resolve<ISongDbManager>();
                songDbManager.AddSingle(song);

                // Act
                songId = songDbManager.GetSongId(song.Title, song.ArtistNames);

                songDbManager.DeleteSingle(songDbManager.GetSongId(song.Title, song.ArtistNames));
            }

            // Assert
            Assert.True(songId > 0);
        }

        [Fact]
        public void GetSongIdReturnsZeroWhenSearchingNonExistentArtists()
        {
            var song = SampleData.GetSampleSingle();

            // Arrange
            var songId = 0;
            var nonExistingArtists = new List<string> {"jsoiapfjdiosjiop", "doafjdsopj"};

            var container = TestContainerConfig.Configure();
            using (var scope = container.BeginLifetimeScope())
            {
                var songDbManager = scope.Resolve<ISongDbManager>();
                songDbManager.AddSingle(song);
                var song2 = song;
                song2.ArtistNames = nonExistingArtists;

                // Act
                songId = songDbManager.GetSongId(song2.Title, song2.ArtistNames);

                songDbManager.DeleteSingle(songDbManager.GetSongId(song.Title, song.ArtistNames));
            }

            // Assert
            Assert.Equal(0, songId);
        }

        [Fact]
        public void SingleGetsAddedToDB()
        {
            // Arrange
            var singleAdded = false;

            var song = SampleData.GetSampleSingle();
            var container = TestContainerConfig.Configure();
            using (var scope = container.BeginLifetimeScope())
            {
                var songDbManager = scope.Resolve<ISongDbManager>();

                // Act
                singleAdded = songDbManager.SingleIsAdded(song);

                songDbManager.DeleteSingle(songDbManager.GetSongId(song.Title, song.ArtistNames));
            }

            // Assert
            Assert.True(singleAdded);
        }
    }
}
