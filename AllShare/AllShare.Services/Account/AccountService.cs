using System;
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
        public AccountService(IUserRepository userRepository)
        {
            UserRepository = userRepository;
        }

        public ServiceResult<UserDTO> GetUser(string username)
        {
            var user = UserRepository.GetUser(username);
            var result = new ServiceResult<UserDTO> {ResultType = user != null ? ResultType.Success : ResultType.Error, Result = TinyMapper.Map<UserDTO>(user) };
            return result;
        }

        public ServiceResult<UserDTO> Register(UserDTO userDto)
        {
            var user = TinyMapper.Map<User>(userDto);
            try
            {
                var newUser = UserRepository.Add(user);
                return new ServiceResult<UserDTO> {ResultType = ResultType.Success, Result = TinyMapper.Map<UserDTO>(newUser)};
            }
            catch (Exception ex)
            {
                return new ServiceResult<UserDTO> {ResultType = ResultType.Error};   
            }
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
    }
}
