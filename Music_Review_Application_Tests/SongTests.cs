using System;
using System.Collections.Generic;
using System.Drawing;
using Music_Review_Application_DB_Managers;
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

            SongDbManager songDbManager = new();
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
            
            singleAdded = SingleIsAdded(new(songTitle, dateOfRelease, img, artistNames, genres), songDbManager);
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
            SongDbManager songDbManager = new();
            songDbManager.DeleteSingle(songDbManager.GetSongId(songTitle, artistNames));
        }

        public bool SingleIsAdded(SingleSong single, SongDbManager songDbManager)
        {
            bool singleAdded = false;

            if (songDbManager.GetSongId(single.Title, single.ArtistNames) == 0)
            {
                songDbManager.AddSingle(single);
                single.Id = songDbManager.GetSongId(single.Title, single.ArtistNames);
                SingleSong single1 = songDbManager.GetSingle(songDbManager.GetSongId(single.Title, single.ArtistNames));

                if (single.Id == single1.Id && single.Img == single1.Img)
                {
                    singleAdded = true;
                }
            }

            return singleAdded;
        }
    }
}
