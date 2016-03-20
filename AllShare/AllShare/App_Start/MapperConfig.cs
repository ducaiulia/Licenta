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
                config.Bind(source => source.FacebookToken, target => target.FacebookToken);
            });

            TinyMapper.Bind<UserDTO, User>(config =>
            {
                config.Bind(source => source.Password, target => target.Password);
                config.Bind(source => source.Username, target => target.Username);
                config.Bind(source => source.FacebookToken, target => target.FacebookToken);
            });

            TinyMapper.Bind<UserDTO, AccountViewModel>(config =>
            {
                config.Bind(source => source.Username, target => target.Username);
                config.Bind(source => source.FacebookToken, target => target.FacebookToken);
            });

            TinyMapper.Bind<AccountInput, UserDTO>(config =>
            {
                config.Bind(source => source.Username, target => target.Username);
                config.Bind(source => source.Password, target => target.Password);
            });

            TinyMapper.Bind<Post, PostDTO>(config =>
            {
                config.Bind(source => source.Text, target => target.Text);
                config.Bind(source => source.DateTime, target => target.DateTime);
                config.Bind(source => source.User, target => target.User);
            });

            TinyMapper.Bind<PostDTO, PostViewModel>(config =>
            {
                config.Bind(source => source.Text, target => target.Text);
                config.Bind(source => source.DateTime, target => target.DateTime);
                config.Bind(source => source.User, target => target.User);
            });
        }
    }
}