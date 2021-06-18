using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Music_Review_Application_Models;

namespace Music_Review_Application_Services.Interfaces
{
    public interface IUserListService
    {
        UserList GetUserListSongsAndAlbums(UserList userList);
    }
}
