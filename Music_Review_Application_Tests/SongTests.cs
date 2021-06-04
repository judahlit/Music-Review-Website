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
            var song = GetSampleSong();
            var songId = 0;
            var container = TestContainerConfig.Configure();

            using (var scope = container.BeginLifetimeScope())
            {
                var songDbManager = scope.Resolve<ISongDbManager>();
                songDbManager.AddSingle(song);
                songId = songDbManager.GetSongId(song.Title, song.ArtistNames);
                songDbManager.DeleteSingle(songDbManager.GetSongId(song.Title, song.ArtistNames));
            }

            Assert.True(songId > 0);
        }

        [Fact]
        public void SingleGetsAddedToDB()
        {
            var singleAdded = false;
            var song = GetSampleSong();
            var container = TestContainerConfig.Configure();

            using (var scope = container.BeginLifetimeScope())
            {
                var songDbManager = scope.Resolve<ISongDbManager>();
                singleAdded = songDbManager.SingleIsAdded(song);
                songDbManager.DeleteSingle(songDbManager.GetSongId(song.Title, song.ArtistNames));
            }

            Assert.True(singleAdded);
        }

        private SingleSong GetSampleSong()
        {
            List<string> artistNames = new();
            List<Genre> genres = new();
            Image img = null;
            var songTitle = "Wren (Initiation Remix)";
            var dateOfRelease = new DateTime(2018, 01, 21);
            artistNames.Add("Faodail");
            artistNames.Add("Initiation");
            genres.Add(new("EDM"));
            genres.Add(new("Dubstep"));
            genres.Add(new("Chillstep"));
            return new SingleSong(songTitle, dateOfRelease, img, artistNames, genres);
        }
    }
}
