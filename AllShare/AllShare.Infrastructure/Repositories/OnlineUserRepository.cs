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
    public class OnlineUserRepository: IOnlineUserRepository
    {
        ApplicationDbContext dbContext = new ApplicationDbContext();
        public async Task Login(string username)
        {
            dbContext.OnlineUsers.Add(new OnlineUser {Username = username});
            await dbContext.SaveChangesAsync();
        }

        public async Task Logout(string username)
        {
            var user = await dbContext.OnlineUsers.FindAsync(username);
            dbContext.OnlineUsers.Remove(user);
            await dbContext.SaveChangesAsync();
        }

        public async Task<List<OnlineUser>> GetOnlineUsers()
        {
            return await dbContext.OnlineUsers.ToListAsync();
        }
    }
}

