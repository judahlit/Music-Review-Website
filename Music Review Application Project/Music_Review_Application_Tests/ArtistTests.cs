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
    }
}
