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
            Assert.True(id > 0);
        }

        [Fact]
        public void ReturnsArtist()
        {
            ArtistDbManager artistDbManager = new();
            Artist artist = artistDbManager.GetArtist(artistDbManager.GetArtistId("Taishi"));
            Assert.Equal("Taishi", artist.ArtistName);
        }
    }
}
