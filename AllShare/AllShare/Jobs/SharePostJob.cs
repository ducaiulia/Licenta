using System;
using System.Linq;
using System.Web.Configuration;
using AllShare.Core.Repositories;
using AllShare.Infrastructure.Repositories;
using AllShare.Services.Social;
using Microsoft.Practices.Unity;
using Quartz;

namespace AllShare.Jobs
{
    public class SharePostJob: IJob
    {
        private static readonly string ConsumerKey = WebConfigurationManager.AppSettings["TwitterConsumerKey"];
        private static readonly string ConsumerSecret = WebConfigurationManager.AppSettings["TwitterConsumerSecret"];

        public void Execute(IJobExecutionContext context)
        {
            var JobRepository = new JobRepository();
            var SocialService = new SocialService();
            var UserRepository = new UserRepository();
            var jobs = JobRepository.GetAllJobs().Where(j => j.Finished.Equals(false));
            var serverPath = "D:\\an 3\\Licenta\\AllShare\\AllShare\\uploadImages";

            foreach (var job in jobs)
            {
                try
                {
                    if (DateTime.Compare(DateTime.Now, job.ToBeRunAt) >= 0)
                    {
                        var user = UserRepository.GetUser(job.CurrentUserId);
                        if (job.IsFacebook)
                        {
                            SocialService.FacebookPost(job.Text, job.AuthorUsername, job.ImagePath, serverPath,
                                user.FacebookToken);
                        }
                        if (job.IsTwitter)
                        {
                            SocialService.SendTweet(job.Text, job.AuthorUsername, job.ImagePath, serverPath, ConsumerKey,
                                ConsumerSecret, job.TwitterAccesToken, job.TwitterSecretToken);
                        }
                    }
                    job.Finished = true;
                }
                catch (Exception ex)
                {
                    job.Finished = false;
                    throw ex;
                }
                finally
                {
                    JobRepository.Delete(job.Id);
                    JobRepository.Add(job);
                }
            }
        }
    }
}