using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AllShare.Core.Domain;
using AllShare.Core.Repositories;
using AllShare.Infrastructure.DatabaseEngine;

namespace AllShare.Infrastructure.Repositories
{
    public class UserRepository: IUserRepository
    {
        ApplicationDbContext dbContext = new ApplicationDbContext();
        public User Add(User user)
        {
            if (dbContext.Users.FirstOrDefault(u => u.Username.Equals(user.Username)) != null)
                throw new Exception("Username already exists");

            var newUser = dbContext.Users.Add(user);
            dbContext.SaveChanges();
            return newUser;
        }

        public void Edit(User user)
        {
            dbContext.Entry(user).State = EntityState.Modified;
            dbContext.SaveChanges();
        }

        public void Remove(int userId)
        {
            var user = dbContext.Users.Find(userId);
            dbContext.Users.Remove(user);
            dbContext.SaveChanges();
        }

        public IList<User> GetAll()
        {
            return dbContext.Users.Include(u => u.Posts).ToList();
        }

        public User GetUser(string username)
        {
            try
            {
                var user = dbContext.Users.Include(u => u.Posts).FirstOrDefault(u => u.Username.Equals(username));
                return user;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public User GetUser(int id)
        {
            var user = dbContext.Users.FirstOrDefault(u => u.Id.Equals(id));

            return user;
        }

        public void SaveFacebookToken(string username, string token)
        {
            var user = dbContext.Users.FirstOrDefault(u => u.Username.Equals(username));
            if (user != null)
                user.FacebookToken = token;

            dbContext.SaveChanges();
        }

        public void SaveTwitterToken(string username, string token, string tokenSecret)
        {
            var user = dbContext.Users.FirstOrDefault(u => u.Username.Equals(username));
            if (user != null)
            {
                user.TwitterToken = token;
                user.TwitterTokenSecret = tokenSecret;
            }

            dbContext.SaveChanges();
        }
    }
}
