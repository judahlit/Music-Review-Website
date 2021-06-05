using System;
using System.Collections.Generic;
using System.Drawing;
using Music_Review_Application_Models;

namespace Music_Review_Application_DB_Tests
{
    public class SampleData
    {
        public static SingleSong GetSampleSingle()
        {
            List<string> artistNames = new();
            List<Genre> genres = new();
            artistNames.Add("Faodail");
            artistNames.Add("Initiation");
            genres.Add(new("EDM"));
            genres.Add(new("Dubstep"));
            genres.Add(new("Chillstep"));
            return new SingleSong("Wren (Initiation Remix)", new DateTime(2018, 01, 21), null, artistNames, genres);
        }

        public static Album GetSampleAlbum()
        {
            Image img = null;
            List<Track> tracks = new();
            List<string> artistNames = new();
            artistNames.Add("Taishi");
            tracks.Add(new Track("Introduction - Somewhere Not in This World", new DateTime(2017, 10, 29), 1, artistNames, new List<Genre> { new("piano"), new("electronic") }));
            tracks.Add(new Track("The Tower Which Is Telling the Time 1", new DateTime(2017, 10, 29), 2, artistNames, new List<Genre> { new("orchestral"), new("electronic") }));
            tracks.Add(new Track("The Tower Which Is Telling the Time 2", new DateTime(2017, 10, 29), 3, artistNames, new List<Genre> { new("orchestral"), new("electronic"), new("EDM") }));
            tracks.Add(new Track("The Tower Which Is Telling the Time 3", new DateTime(2017, 10, 29), 4, artistNames, new List<Genre> { new("orchestral"), new("electronic"), new("EDM"), new("Trance") }));
            tracks.Add(new Track("Encounter Like a Rendezvous (in Another World)", new DateTime(2017, 10, 29), 5, artistNames, new List<Genre> { new("piano") }));
            List<string> albumArtists = new();
            albumArtists.Add("Taishi");
            return new("Somewhere Not in This World", tracks, new DateTime(2020, 04, 20), img, albumArtists);
        }
    }
}
