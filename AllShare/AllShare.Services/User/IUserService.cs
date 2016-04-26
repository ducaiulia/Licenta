using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AllShare.Services.DTOs;

namespace AllShare.Services.User
{
    public interface IUserService
    {
        Task<ServiceResult<List<UserDTO>>> GetOnlineUsers();
    }
}
