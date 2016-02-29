using System;
using System.Collections.Generic;
using System.Linq;
using AllShare.Core.Domain;
using AllShare.Core.Repositories;
using AllShare.Infrastructure.DatabaseEngine;

namespace AllShare.Infrastructure.Repositories
{
    public class PostRepository: IPostRepository
    {
        public IList<Post> GetAll()
        {
            using (var dbContext = new ApplicationDbContext())
            {
                return dbContext.Posts.OrderBy(p => p.DateTime).ToList();
            }
        }

        public Post Add(Post post)
        {
            using (var dbContext = new ApplicationDbContext())
            {
                dbContext.Posts.Add(post);
                dbContext.SaveChanges();
                return post;
            }
        }
    }
}
