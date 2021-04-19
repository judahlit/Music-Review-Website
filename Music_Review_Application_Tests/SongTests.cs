using Music_Review_Application_LIB;
using Music_Review_Application_LIB.DbManagers;
using System;
using System.Collections.Generic;
using Xunit;
using Single = Music_Review_Application_LIB.Single;

namespace Music_Review_Application_Tests
{
    public class SongTests
    {
        [Fact]
        public void SingleGetsAddedToDB()
        {
            bool singleAdded = false;

            SongDbManager songDbManager = new();
            List<string> artistNames = new();
            List<string> genres = new();
            byte[] img = null;

            artistNames.Add("KROWW");
            artistNames.Add("QroH");
            genres.Add("EDM");
            genres.Add("Dubstep");
            genres.Add("Deathstep");

            Single single = new("The Desert's Curse", new DateTime(2020, 04, 20), img, artistNames, genres);

            if (songDbManager.GetSongId(single.Title, single.DateOfRelease) == 0)
            {
                songDbManager.AddSingle(single);
                Single single1 = songDbManager.GetSingle(songDbManager.GetSongId("The Desert's Curse", new DateTime(2020, 04, 20)));

                if (single1.Id > 0 && single1.ArtistNames.Count == 2 && single1.GenreNames.Count == 3)
                {
                    singleAdded = true;
                }
            }

            Assert.True(singleAdded);
        }
        /*
        [Fact]
        public void PartOfAlbumSongGetsAddedToDB()
        {

        }
        [Fact]
        public void GetsSongIdBySearcingSongTitleAndArtists()
        {

        }

        [Fact]
        public void GetsSongBySongId()
        {

        }

        [Fact]
        public void DeleteSongFromDB()
        {
            SongDbManager songDbManager = new();
            songDbManager.

        }
        */
    }
}
