using AutoMapper;
using Music_Review_Application_GUI.Models;
using Music_Review_Application_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Music_Review_Application_GUI
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Album, AlbumViewModel>();
            CreateMap<AlbumReview, AlbumReviewViewModel>();
            CreateMap<Artist, ArtistViewModel>();
            CreateMap<Song, SongViewModel>();
            CreateMap<SongReview, SongReviewViewModel>();
            CreateMap<UserList, UserListViewModel>();
        }
    }
}
