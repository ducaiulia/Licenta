using AllShare.Services.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllShare.Services.Social
{
    public interface ISocialService
    {
        Task FacebookPost(string text, string username, string imagePath, string serverPath, string fbToken);
        Task SendTweet(string text, string username, string imagePath, string serverPath, string consumerKey, string consumerSecret, string twToken, string twTokenSecret);

        void AddJob(string text, int userId, string authorUsername, string imagePath, DateTime toBeRunAt, bool isFacebook);
        IList<JobDTO> GetNotRunJobs(int userId);
    }
}
