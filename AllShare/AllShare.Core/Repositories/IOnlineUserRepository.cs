using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AllShare.Core.Domain;

namespace AllShare.Core.Repositories
{
    public interface IOnlineUserRepository
    {
        Task Login(string username);
        Task Logout(string username);
        Task<List<OnlineUser>> GetOnlineUsers();
    }
}
