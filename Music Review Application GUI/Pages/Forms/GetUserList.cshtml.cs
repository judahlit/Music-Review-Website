using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Music_Review_Application_DB_Managers.Interfaces;
using Music_Review_Application_GUI.Models;

namespace Music_Review_Application_GUI.Pages.Forms
{
    public class GetUserListModel : PageModel
    {
        private readonly IMapper _mapper;
        private readonly IUserListDbManager _userListDbManager;
        private readonly IAlbumDbManager _albumDbManager;
        private readonly ISongDbManager _songDbManager;

        [BindProperty]
        public UserListViewModel UserList { get; private set; }

        public GetUserListModel(IMapper mapper, IUserListDbManager userListDbManager, IAlbumDbManager albumDbManager, ISongDbManager songDbManager)
        {
            _mapper = mapper;
            _userListDbManager = userListDbManager;
            _albumDbManager = albumDbManager;
            _songDbManager = songDbManager;
        }

        public void OnGet(string username)
        {
            UserList = _mapper.Map<UserListViewModel>(_userListDbManager.GetUserList(username));
            UserList.SongReviews.ForEach(sr => sr.Song = _mapper.Map<SongViewModel>(_songDbManager.GetSong(sr.SongId)));
            UserList.AlbumReviews.ForEach(ar => ar.Album = _mapper.Map<AlbumViewModel>(_albumDbManager.GetAlbum(ar.AlbumId)));
        }
    }
}
