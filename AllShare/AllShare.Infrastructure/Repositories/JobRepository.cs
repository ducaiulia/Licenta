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
    public class JobRepository: IJobRepository
    {
        ApplicationDbContext dbContext = new ApplicationDbContext();

        public void Add(SharePostJobModel jobModel)
        {
            dbContext.Jobs.Add(jobModel);
            dbContext.SaveChanges();
        }

        public IList<SharePostJobModel> GetAllJobs()
        {
            return dbContext.Jobs.OrderBy(x => x.ToBeRunAt).ToList();
        }

        public IList<SharePostJobModel> GetAllNotRunJobsForUser(int userId)
        {
            return dbContext.Jobs.Where(x => x.CurrentUserId.Equals(userId) && x.Finished.Equals(false)).OrderBy(y => y.ToBeRunAt).ToList();
        }

        public void Delete(int jobId)
        {
            var job = dbContext.Jobs.First(p => p.Id.Equals(jobId));
            if (job != null)
            {
                try
                {
                    dbContext.Jobs.Remove(job);
                    dbContext.SaveChanges();
                }
                catch (Exception ex)
                {
                    // ignored
                }
            }
        }
    }
}
