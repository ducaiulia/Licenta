using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AllShare.Core.Domain;
using AllShare.Models;
using AllShare.Services.DTOs;
using Nelibur.ObjectMapper;

namespace AllShare.App_Start
{
    public static class MapperConfig
    {
        public static void Register()
        {
            TinyMapper.Bind<User, UserDTO>(config =>
            {
                config.Bind(source => source.Password, target => target.Password);
                config.Bind(source => source.Username, target => target.Username);
            });

            TinyMapper.Bind<UserDTO, User>(config =>
            {
                config.Bind(source => source.Password, target => target.Password);
                config.Bind(source => source.Username, target => target.Username);
            });

            TinyMapper.Bind<UserDTO, AccountViewModel>(config =>
            {
                config.Bind(source => source.Username, target => target.Username);
            });

            TinyMapper.Bind<AccountInput, UserDTO>(config =>
            {
                config.Bind(source => source.Username, target => target.Username);
                config.Bind(source => source.Password, target => target.Password);
            });
        }
    }
}