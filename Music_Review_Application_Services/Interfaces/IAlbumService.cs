using Music_Review_Application_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music_Review_Application_Services.Interfaces
{
    public interface IAlbumService
    {
        List<Album> GetAlbumsFromGenreSongs(List<Song> songs);
        List<Genre> GetAlbumGenres(Album album);
    }
}
