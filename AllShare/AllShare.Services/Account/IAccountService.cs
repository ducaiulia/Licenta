using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AllShare.Services.DTOs;

namespace AllShare.Services.Account
{
    public interface IAccountService
    {
        Task<ServiceResult<UserDTO>> GetUser(string username);

        Task<ServiceResult<UserDTO>> Register(UserDTO user);

        Task Logout(string username);

        ServiceResult<bool> SaveFacebookToken(string username, string token);

        ServiceResult<bool> SaveTwitterToken(string username, string token, string tokenSecret);
    }
}
