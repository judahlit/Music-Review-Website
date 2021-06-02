using System;
using System.Collections.Generic;
using Music_Review_Application_DB_Managers;
using Music_Review_Application_DB_Managers.Interfaces;
using Music_Review_Application_Models;
using Xunit;

namespace Music_Review_Application_Tests
{
    public class ArtistTests
    {
        [Fact]
        public void ReturnsArtistId()
        {
            IArtistDbManager _artistDbManager;
            int id = _artistDbManager.GetArtistId("Taishi");
            Assert.True(id > 0);
        }

        [Fact]
        public void ReturnsArtist()
        {
            IArtistDbManager _artistDbManager;
            Artist artist = _artistDbManager.GetArtist(_artistDbManager.GetArtistId("Taishi"));
            Assert.Equal("Taishi", artist.ArtistName);
        }
    }
}
