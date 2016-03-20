using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace AllShare.Controllers
{
    public class SocialController : Controller
    {
        [HttpPost]
        public async Task FacebookPost(string text, string username)
        {
            var description = text + " - @" + username;
            var facebookClient = new Facebook.FacebookClient(Session["FacebookAccessToken"].ToString());
            var postParams = new { message = description };

            //try
            //{
            //    dynamic fbPostTaskResult = await facebookClient.PostTaskAsync("/me/feed", postParams);
                
            //    var result = (IDictionary<string, object>)fbPostTaskResult;
            //}
            //catch (Exception ex)
            //{
            //      throw ex;
            //}
        }
    }
}