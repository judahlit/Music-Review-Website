using Music_Review_Application_LIB;
using Music_Review_Application_LIB.DbManagers;
using System;
using System.Collections.Generic;
using Xunit;

namespace Music_Review_Application_Tests
{
    public class ArtistTests
    {
        [Fact]
        public void ReturnsArtistId()
        {
            ArtistDbManager artistDbManager = new();
            int id = artistDbManager.GetArtistId("Taishi");
            Assert.Equal(1, id);
        }

        [Fact]
        public void AddAnArtistToDB()
        {
            bool artistAdded = false;

            ArtistDbManager artistDbManager = new();
            Artist artist = new("KROWW", null, "Orchestrial dubstep producer.");
            artistDbManager.AddArtist(artist);
            int artistId = artistDbManager.GetArtistId(artist.ArtistName);

            if (artistId > 0)
            {
                artistAdded = true;
            }

            Assert.True(artistAdded);
        }
    }
}
