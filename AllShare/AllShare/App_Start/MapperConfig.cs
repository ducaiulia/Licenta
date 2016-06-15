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
                config.Bind(source => source.TwitterToken, target => target.TwitterToken);
                config.Bind(source => source.TwitterTokenSecret, target => target.TwitterTokenSecret);
                config.Bind(source => source.Id, target => target.UserId);
            });

            TinyMapper.Bind<UserDTO, User>(config =>
            {
                config.Bind(source => source.Password, target => target.Password);
                config.Bind(source => source.Username, target => target.Username);
                config.Bind(source => source.FacebookToken, target => target.FacebookToken);
                config.Bind(source => source.TwitterToken, target => target.TwitterToken);
                config.Bind(source => source.TwitterTokenSecret, target => target.TwitterTokenSecret);
                config.Bind(source => source.UserId, target => target.Id);
            });

            TinyMapper.Bind<OnlineUser, UserDTO>(config =>
            {
                config.Bind(source => source.Username, target => target.Username);
            });

            TinyMapper.Bind<UserDTO, AccountViewModel>(config =>
            {
                config.Bind(source => source.Username, target => target.Username);
                config.Bind(source => source.FacebookToken, target => target.FacebookToken);
                config.Bind(source => source.TwitterToken, target => target.TwitterToken);
                config.Bind(source => source.TwitterTokenSecret, target => target.TwitterTokenSecret);
                config.Bind(source => source.UserId, target => target.UserId);
            });

            TinyMapper.Bind<AccountInput, UserDTO>(config =>
            {
                config.Bind(source => source.Username, target => target.Username);   
                config.Bind(source => source.Email, target => target.Email);
                config.Bind(source => source.Password, target => target.Password);
                config.Bind(source => source.FirstName, target => target.FirstName);
                config.Bind(source => source.LastName, target => target.LastName);
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
                config.Bind(source => source.ImagePath, target => target.ImagePath);
                config.Bind(source => source.IsFile, target => target.IsFile);
            });

            TinyMapper.Bind<PostDTO, PostViewModel>(config =>
            {
                config.Bind(source => source.Text, target => target.Text);
                config.Bind(source => source.DateTime, target => target.DateTime);
                config.Bind(source => source.User, target => target.User);
            });

            TinyMapper.Bind<SharePostJobModel, JobDTO>(config =>
            {
                config.Bind(source => source.Id, target => target.Id);
                config.Bind(source => source.ImagePath, target => target.ImagePath);
                config.Bind(source => source.AuthorUsername, target => target.AuthorUsername);
                config.Bind(source => source.IsFacebook, target => target.IsFacebook);
                config.Bind(source => source.IsTwitter, target => target.IsTwitter);
                config.Bind(source => source.ToBeRunAt, target => target.ToBeRunAt);
                config.Bind(source => source.Text, target => target.Text);
            });
        }
    }
}