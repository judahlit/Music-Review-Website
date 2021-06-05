using System;
using System.Collections.Generic;
using Autofac;
using Music_Review_Application_DB_Managers.Interfaces;
using Music_Review_Application_Models;
using Xunit;
using System.Linq;
using Castle.Components.DictionaryAdapter;

namespace Music_Review_Application_Tests
{
    public class GenreTests
    {
        [Fact]
        public void ReturnsAListOfSongsWithASpecifiedGenre()
        {
            // Arrange
            var tracks = new List<Track>();

            var songs = new List<Song>();
            var album = SampleData.GetSampleAlbum();
            var container = TestContainerConfig.Configure();

            using (var scope = container.BeginLifetimeScope())
            {
                var songDbManager = scope.Resolve<ISongDbManager>();
                var albumDbManager = scope.Resolve<IAlbumDbManager>();
                var genreDbManager = scope.Resolve<IGenreDbManager>();

                albumDbManager.AddAlbum(album);
                var albumId = albumDbManager.GetAlbumId(album.Title, album.ArtistNames);

                songs = songDbManager.GetSongsWithGenre(genreDbManager.GetGenreId("orchestral"))
                    .Where(s => s.GetType() == typeof(Track))
                    .ToList();

                // Act
                tracks = songs.Cast<Track>().ToList()
                    .Where(t => t.AlbumId == albumId)
                    .ToList();


                albumDbManager.DeleteAlbum(albumId);
            }

            // Assert
            Assert.Equal(3, tracks.Count);
        }
    }
}
