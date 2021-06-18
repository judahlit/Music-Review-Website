using Music_Review_Application_DB_Managers.Interfaces;
using Music_Review_Application_Models;
using Music_Review_Application_Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music_Review_Application_Services
{
    public class AlbumService : IAlbumService
    {
        private readonly IAlbumDbManager _albumDbManager;

        public AlbumService(IAlbumDbManager albumDbManager)
        {
            _albumDbManager = albumDbManager;
        }

        public List<Genre> GetAlbumGenres(Album album)
        {
            return album.GetAlbumGenres();
        }

        public List<Album> GetAlbumsFromGenreSongs(List<Song> songs)
        {
            var albums = new List<Album>();

            foreach (var song in songs)
            {
                if (song.GetType() == typeof(Track))
                {
                    var track = (Track)song;
                    if (track.AlbumId > 0 && !albums.Select(a => a.Id).Contains(track.AlbumId))
                    {
                        albums.Add(_albumDbManager.GetAlbum(track.AlbumId));
                    }
                }
            }

            return albums;
        }
    }
}
