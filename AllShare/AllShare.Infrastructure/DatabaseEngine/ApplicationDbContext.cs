using System.Data.Entity;
using System.Diagnostics;
using AllShare.Core.Domain;

namespace AllShare.Infrastructure.DatabaseEngine
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext() : base("AllShareContext")
        {
            Database.Log = s => Trace.Write(s);
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<OnlineUser> OnlineUsers { get; set; }
        public DbSet<SharePostJobModel> Jobs { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Post>().HasRequired(p => p.User)
                .WithMany(b => b.Posts)
                .HasForeignKey(p => p.UserId);
        }
    }
}
