using Music_Review_Application_LIB.DbManagers;
using Music_Review_Application_LIB;
using System;
using System.Collections.Generic;
using Xunit;
using System.Drawing;

namespace Music_Review_Application_Tests
{
    public class AlbumTests
    {
        [Fact]
        public void AlbumGetsAddedToDB()
        {
            bool albumAdded = false;

            AlbumDbManager albumDbManager = new();
            SongDbManager songDbManager = new();
            Image img = null;

            List<Track> tracks = new();
            List<string> albumArtistNames = new();
            List<List<string>> artistNames = new();
            List<List<string>> genreNames = new();

            genreNames[0].Add("EDM");
            genreNames[0].Add("Dubstep");
            genreNames[0].Add("Deathstep");

            for (int i = 0; i < 6; i++)
            {
                artistNames[i].Add("KROWW");
            }

            artistNames[4].Add("Pure Karnage");
            /*
            tracks.Add(new Track("Hybrid Empire (The Crawling Chaos VIP)", new DateTime(2018, 08, 05), 1, artistNames[0], genreNames[0]));
            tracks.Add(new Track("The First Mech", new DateTime(2018, 08, 05), 2, artistNames[1], genreNames[0]));
            tracks.Add(new Track("Fear Me", new DateTime(2018, 08, 05), 3, artistNames[2], genreNames[0]));
            tracks.Add(new Track("Divine Power", new DateTime(2018, 08, 05), 4, artistNames[3], genreNames[0]));
            tracks.Add(new Track("Obsolete Existence", new DateTime(2018, 08, 05), 5, artistNames[4], genreNames[0]));
            tracks.Add(new Track("The Desert's Curse", new DateTime(2018, 08, 05), 6, artistNames[5], genreNames[0]));

            Album album = new("Almost Human", tracks, new DateTime(2020, 04, 20), img, albumArtistNames);
            
            if (albumDbManager.GetAlbumId(album.Title, album.DateOfRelease) == 0)
            {
                albumDbManager.Add(album);
                Album album1 = songDbManager.GetSingle(songDbManager.GetSongId("The Desert's Curse", new DateTime(2020, 04, 20)));

                if (album.Id > 0 && album.ArtistNames.Count == 2 && album.GenreNames.Count == 3)
                {
                    albumAdded = true;
                }
            }

            Assert.True(albumAdded);*/
        }
    }
}
