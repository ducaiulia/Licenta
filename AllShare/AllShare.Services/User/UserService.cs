using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AllShare.Core.Repositories;
using AllShare.Services.DTOs;
using AllShare.Services.Utils;
using Nelibur.ObjectMapper;

namespace AllShare.Services.User
{
    public class UserService: IUserService
    {
        private IOnlineUserRepository OnlineUserRepository { get; set; }

        public UserService(IOnlineUserRepository onlineUserRepository)
        {
            OnlineUserRepository = onlineUserRepository;
        }

        public async Task<ServiceResult<List<UserDTO>>> GetOnlineUsers()
        {
            try
            {
                var onlineUsers = await OnlineUserRepository.GetOnlineUsers();
                return new ServiceResult<List<UserDTO>>
                {
                    ResultType = ResultType.Success,
                    Result = TinyMapper.Map<List<UserDTO>>(onlineUsers)
                };
            }
            catch (Exception ex)
            {
                return new ServiceResult<List<UserDTO>> {ResultType = ResultType.Error};
            }
        }
    }
}
