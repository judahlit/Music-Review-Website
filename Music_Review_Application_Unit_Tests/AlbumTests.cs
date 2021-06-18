using System;
using Music_Review_Application_Sample_Data;
using Xunit;

namespace Music_Review_Application_Unit_Tests
{
    public class AlbumTests
    {
        [Fact]
        public void GetAlbumGenresReturnsRightAmountOfGenres()
        {
            var album = SampleData.GetSampleAlbum();
            var genreAmount = album.GetAlbumGenres().Count;
            Assert.Equal(5, genreAmount);
        }
    }
}
