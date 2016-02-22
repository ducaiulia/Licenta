using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AllShare.Core.Domain;

namespace AllShare.Infrastructure.DatabaseEngine
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext() : base("AllShareContext")
        { }

        public DbSet<User> Users { get; set; }
    }
}
