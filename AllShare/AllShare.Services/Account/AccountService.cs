using System;
using System.Threading.Tasks;
using AllShare.Core.Domain;
using AllShare.Core.Repositories;
using AllShare.Services.DTOs;
using AllShare.Services.Utils;
using Microsoft.Practices.Unity;
using Nelibur.ObjectMapper;

namespace AllShare.Services.Account
{
    public class AccountService: IAccountService
    {
        private IUserRepository UserRepository { get; set; }
        private IOnlineUserRepository OnlineUserRepository { get; set; }
        public AccountService(IUserRepository userRepository, IOnlineUserRepository onlineUserRepository)
        {
            UserRepository = userRepository;
            OnlineUserRepository = onlineUserRepository;
        }

        public async Task<ServiceResult<UserDTO>> GetUser(string username)
        {
            var user = UserRepository.GetUser(username);
            if (user != null)
            {
                await OnlineUserRepository.Login(username);
                return new ServiceResult<UserDTO>
                {
                    ResultType = ResultType.Success,
                    Result = TinyMapper.Map<UserDTO>(user)
                };
            }

            return new ServiceResult<UserDTO> {ResultType = ResultType.Error};
        }

        public async Task<ServiceResult<UserDTO>> Register(UserDTO userDto)
        {
            var user = TinyMapper.Map<Core.Domain.User>(userDto);
            try
            {
                var newUser = UserRepository.Add(user);
                await OnlineUserRepository.Login(newUser.Username);
                return new ServiceResult<UserDTO> {ResultType = ResultType.Success, Result = TinyMapper.Map<UserDTO>(newUser)};
            }
            catch (Exception ex)
            {
                return new ServiceResult<UserDTO> {ResultType = ResultType.Error};   
            }
        }

        public async Task Logout(string username)
        {
            await OnlineUserRepository.Logout(username);
        }

        public ServiceResult<bool> SaveFacebookToken(string username, string token)
        {
            try
            {
                UserRepository.SaveFacebookToken(username, token);
                return new ServiceResult<bool> { ResultType = ResultType.Success };
            }
            catch (Exception ex)
            {
                return new ServiceResult<bool> { ResultType = ResultType.Error };
            }
        }

        public ServiceResult<bool> SaveTwitterToken(string username, string token, string tokenSecret)
        {
            try
            {
                UserRepository.SaveTwitterToken(username, token, tokenSecret);
                return new ServiceResult<bool>() {ResultType = ResultType.Success};
            }
            catch (Exception ex)
            {
                return new ServiceResult<bool>() {ResultType = ResultType.Error};
            }
        }
    }
}
