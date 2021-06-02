using System;
using System.Collections.Generic;
using System.Drawing;
using Music_Review_Application_DB_Managers;
using Music_Review_Application_DB_Managers.Interfaces;
using Music_Review_Application_Models;
using Xunit;

namespace Music_Review_Application_Tests
{
    public class SongTests
    {
        [Fact]
        public void SingleGetsAddedToDB()
        {
            bool singleAdded;

            ISongDbManager _songDbManager;
            List<string> artistNames = new();
            List<Genre> genres = new();

            Image img = Image.FromFile(@"D:\Users\Judah\Pictures\Music Related\Song Pics\Wren (remix).jpg");
            //Image img = null;

            string songTitle = "Wren (Initiation Remix)";
            DateTime dateOfRelease = new DateTime(2018, 01, 21);

            artistNames.Add("Faodail");
            artistNames.Add("Initiation");
            genres.Add(new("EDM"));
            genres.Add(new("Dubstep"));
            genres.Add(new("Chillstep"));

            singleAdded = _songDbManager.SingleIsAdded(new(songTitle, dateOfRelease, img, artistNames, genres));
            DeleteSongFromDB(songTitle, artistNames);

            Assert.True(singleAdded);
        }

        [Fact]
        public void GetsSongIdBySearchingSongTitleAndArtists()
        {

        }

        [Fact]
        public void GetsSongBySongId()
        {

        }

        public void DeleteSongFromDB(string songTitle, List<string> artistNames)
        {
            ISongDbManager _songDbManager;
            _songDbManager.DeleteSingle(_songDbManager.GetSongId(songTitle, artistNames));
        }
    }
}
