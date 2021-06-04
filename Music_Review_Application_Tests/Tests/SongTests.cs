using System;
using System.Collections.Generic;
using System.Drawing;
using Autofac;
using Moq;
using Music_Review_Application_DB_Managers;
using Music_Review_Application_DB_Managers.Interfaces;
using Music_Review_Application_Models;
using Xunit;

namespace Music_Review_Application_Tests
{
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
