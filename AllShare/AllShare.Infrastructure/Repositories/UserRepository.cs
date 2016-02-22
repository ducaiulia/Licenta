using System;
using System.Collections.Generic;
using System.Data.Entity;
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
        public void Add(User user)
        {
            dbContext.Users.Add(user);
        }

        public void Edit(User user)
        {
            dbContext.Entry(user).State = EntityState.Modified;
        }

        public void Remove(int userId)
        {
            var user = dbContext.Users.Find(userId);
            dbContext.Users.Remove(user);
            dbContext.SaveChanges();
        }

        public IList<User> GetAll()
        {
            return dbContext.Users.ToList();
        }
    }
}
