using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using AllShare.Models;
using TweetSharp;

namespace AllShare.Controllers
{
    public class SocialController : Controller
    {
        private static readonly string ConsumerKey = WebConfigurationManager.AppSettings["TwitterConsumerKey"];
        private static readonly string ConsumerSecret = WebConfigurationManager.AppSettings["TwitterConsumerSecret"];

        [HttpPost]
        public async Task FacebookPost(string text, string username, string imagePath)
        {
            var description = String.Format("{0} - {1}", text, username);
            var facebookClient = new Facebook.FacebookClient(Session["FacebookAccessToken"].ToString());

            object postParams;

            if (!String.IsNullOrEmpty(imagePath))
            {
                var split = imagePath.Split('/');

                string localFilename = System.IO.Path.Combine(
                    Server.MapPath("~/uploadImages"), split.Last());

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
                postParams = new {message = description};

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

        [HttpPost]
        public async Task SendTweet(string text, string username, string imagePath)
        {
            var description = String.Format("{0} - {1}", text, username);
            TwitterService service = new TwitterService(ConsumerKey, ConsumerSecret);

            var viewModel = (AccountViewModel) Session["User"];

            service.AuthenticateWith(viewModel.TwitterToken, viewModel.TwitterTokenSecret);

            if (!String.IsNullOrEmpty(imagePath))
            {
                var split = imagePath.Split('/');

                string localFilename = System.IO.Path.Combine(
                    Server.MapPath("~/uploadImages"), split.Last());
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
    }
}