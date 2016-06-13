using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AllShare.Core.Domain;

namespace AllShare.Core.Repositories
{
    public interface IJobRepository
    {
        void Add(SharePostJobModel jobModel);
        IList<SharePostJobModel> GetAllJobs();
        IList<SharePostJobModel> GetAllNotRunJobsForUser(int userId);
    }
}
