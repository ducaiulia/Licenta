using AllShare.Core.Domain;

namespace AllShare.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<AllShare.Infrastructure.DatabaseEngine.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(AllShare.Infrastructure.DatabaseEngine.ApplicationDbContext context)
        {
            context.Users.Add(new User
            {
                FirstName = "Iulia",
                LastName = "Duca",
                Password = "pass",
                Username = "ducaiulia"
            });
            base.Seed(context);
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
        }
    }
}
