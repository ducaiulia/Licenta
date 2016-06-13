using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AllShare.Core.Domain;
using AllShare.Core.Repositories;
using AllShare.Services.DTOs;
using AllShare.Services.Social;
using Microsoft.Practices.Unity;
using Nelibur.ObjectMapper;
using TweetSharp;

namespace AllShare.Services.Social
{
    public class SocialService: ISocialService
    {
        [Dependency]
        public IJobRepository JobRepository { get; set; }
        [Dependency]
        public IUserRepository UserRepository { get; set; }

        public async Task FacebookPost(string text, string username, string imagePath, string serverPath, string fbToken)
        {
            var description = String.Format("{0} - by: {1}", text, username);
            var facebookClient = new Facebook.FacebookClient(fbToken);

            object postParams;

            if (!String.IsNullOrEmpty(imagePath))
            {
                var split = imagePath.Split('/');
                string localFilename = System.IO.Path.Combine(serverPath, split.Last());

                if (!System.IO.File.Exists(localFilename))
                {
                    using (WebClient client = new WebClient())
                    {
                        client.DownloadFile(new Uri(imagePath), localFilename);
                    }
                }

                var sourceFile = new Facebook.FacebookMediaObject
                {
                    ContentType = "image/jpeg",
                    FileName = Path.GetFileName(localFilename)
                }.SetValue(System.IO.File.ReadAllBytes(localFilename));

                postParams = new { message = description, source = sourceFile };
            }
            else
                postParams = new { message = description };

            try
            {
                dynamic fbPostTaskResult = await facebookClient.PostTaskAsync("/me/photos", postParams);

                var result = (IDictionary<string, object>)fbPostTaskResult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task SendTweet(string text, string username, string imagePath, string serverPath, string consumerKey, string consumerSecret, string twToken, string twTokenSecret)
        {
            var description = String.Format("{0} - by: {1}", text, username);
            TwitterService service = new TwitterService(consumerKey, consumerSecret);

            service.AuthenticateWith(twToken, twTokenSecret);

            if (!String.IsNullOrEmpty(imagePath))
            {
                var split = imagePath.Split('/');
                string localFilename = System.IO.Path.Combine(serverPath, split.Last());

                if (!System.IO.File.Exists(localFilename))
                {
                    using (WebClient client = new WebClient())
                    {
                        client.DownloadFile(new Uri(imagePath), localFilename);
                    }
                }
                var stream = new FileStream(localFilename, FileMode.Open);
                try
                {
                    var mediaResult = service.SendTweetWithMedia(new SendTweetWithMediaOptions
                    {
                        Status = description,
                        Images = new Dictionary<string, Stream>
                        {
                            {Guid.NewGuid().ToString(), stream}
                        }
                    });
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            else
            {
                TwitterStatus result = service.SendTweet(new SendTweetOptions
                {
                    Status = description
                });
            }
        }

        public void AddJob(string text, int userId, string authorUsername, string imagePath, DateTime toBeRunAt, bool isFacebook)
        {
            var user = UserRepository.GetUser(userId);

            JobRepository.Add(new SharePostJobModel
            {
                Text = text,
                CurrentUserId = userId,
                AuthorUsername = authorUsername,
                ImagePath = imagePath,
                ToBeRunAt = toBeRunAt,
                IsTwitter = !isFacebook,
                IsFacebook = isFacebook,
                Finished = false,
                FacebookToken = user.FacebookToken,
                TwitterAccesToken = user.TwitterToken,
                TwitterSecretToken = user.TwitterTokenSecret
            });
        }

        public IList<JobDTO> GetNotRunJobs(int userId)
        {
            var jobsModel = JobRepository.GetAllNotRunJobsForUser(userId);
            return TinyMapper.Map<List<JobDTO>>(jobsModel);
        }
    }
}
