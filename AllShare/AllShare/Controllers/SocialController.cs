using System;
using System.Threading.Tasks;
using System.Web.Configuration;
using System.Web.Mvc;
using AllShare.Models;
using AllShare.Services.Social;
using Microsoft.Practices.Unity;

namespace AllShare.Controllers
{
    public class SocialController : Controller
    {
        [Dependency]
        public ISocialService SocialService { get; set; }

        private static readonly string ConsumerKey = WebConfigurationManager.AppSettings["TwitterConsumerKey"];
        private static readonly string ConsumerSecret = WebConfigurationManager.AppSettings["TwitterConsumerSecret"];

        [HttpPost]
        public async Task FacebookPost(string text, string username, string imagePath)
        {
            await
                SocialService.FacebookPost(text, username, imagePath, Server.MapPath("~/uploadImages"),
                    (string) Session["FacebookAccessToken"]);
        }

        [HttpPost]
        public async Task SendTweet(string text, string username, string imagePath)
        {
            var viewModel = (AccountViewModel)Session["User"];
            await
                SocialService.SendTweet(text, username, imagePath, Server.MapPath("~/uploadImages"), ConsumerKey,
                    ConsumerSecret, viewModel.TwitterToken, viewModel.TwitterTokenSecret);
        }

        [HttpPost]
        public async Task ScheduleJob(string text, string username, string imagePath, string toRun)
        {
            var viewModel = (AccountViewModel)Session["User"];
            SocialService.AddJob(text, viewModel.UserId, username, imagePath, DateTime.Parse(toRun), false);
        }
    }
}