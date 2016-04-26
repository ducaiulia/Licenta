using System;
using System.Collections.Generic;
using System.Linq;
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
        public async Task FacebookPost(string text, string username)
        {
            var description = String.Format("{0} - {1}", text, username);
            var facebookClient = new Facebook.FacebookClient(Session["FacebookAccessToken"].ToString());
            var postParams = new { message = description };

            try
            {
                dynamic fbPostTaskResult = await facebookClient.PostTaskAsync("/me/feed", postParams);

                var result = (IDictionary<string, object>)fbPostTaskResult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public async Task SendTweet(string text, string username)
        {
            var description = String.Format("{0} - {1}", text, username);
            TwitterService service = new TwitterService(ConsumerKey, ConsumerSecret);

            var viewModel = (AccountViewModel) Session["User"];

            service.AuthenticateWith(viewModel.TwitterToken, viewModel.TwitterTokenSecret);
            TwitterStatus result = service.SendTweet(new SendTweetOptions
            {
                Status = description
            });
        }
    }
}