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
        ServiceResult<UserDTO> GetUser(string username);

        ServiceResult<UserDTO> Register(UserDTO user);

        ServiceResult<bool> SaveFacebookToken(string username, string token);
    }
}
