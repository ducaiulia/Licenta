using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AllShare.Core.Domain;

namespace AllShare.Core.Repositories
{
    public interface IUserRepository
    {
        User Add(User user);
        void Edit(User user);
        void Remove(int userId);
        IList<User> GetAll();
        User GetUser(string username);
    }
}
