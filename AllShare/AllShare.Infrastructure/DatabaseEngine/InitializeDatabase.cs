using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AllShare.Core.Domain;

namespace AllShare.Infrastructure.DatabaseEngine
{
    public class InitializeDatabase: DropCreateDatabaseIfModelChanges<ApplicationDbContext>
    {
        //protected override void Seed(ApplicationDbContext dbContext)
        //{
        //    dbContext.Users.Add(new User
        //    {
        //        FirstName = "Iulia",
        //        LastName = "Duca",
        //        Password = "pass",
        //        Username = "ducaiulia"
        //    });
        //    base.Seed(dbContext);
        //}
    }
}
