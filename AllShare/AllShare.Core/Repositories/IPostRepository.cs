using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AllShare.Core.Domain;

namespace AllShare.Core.Repositories
{
    public interface IPostRepository
    {
        IList<Post> GetAll();

        Post Add(Post post);
    }
}
