using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AllShare.Core.Domain;

namespace AllShare.Infrastructure.DatabaseEngine
{
    public class InitializeDatabase: DropCreateDatabaseAlways<ApplicationDbContext>
    {
        //protected override void Seed(ApplicationDbContext dbContext)
        //{
            
        //    dbContext.Users.Add(new User
        //    {
        //        FirstName = "Iulia",
        //        LastName = "Duca",
        //        Password = "AP6FtqHWg4ZsziY0Vd63DP6Khuz37VVgemuWsuBw3CtO5KK/dwTU0UXE13cfIieoKA==",
        //        Username = "ducaiulia"
        //    });
        //    base.Seed(dbContext);
        //}
    }
}
