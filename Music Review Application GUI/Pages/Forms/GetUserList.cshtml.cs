using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        public UserListViewModel UserList { get; private set; }

        public GetUserListModel(IMapper mapper, IUserListDbManager userListDbManager)
        {
            _mapper = mapper;
            _userListDbManager = userListDbManager;
        }

        public void OnGet(string username)
        {
            UserList = _mapper.Map<UserListViewModel>(_userListDbManager.GetUserList(username));
        }
    }
}
