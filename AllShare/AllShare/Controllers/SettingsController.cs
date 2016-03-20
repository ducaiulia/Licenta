using System;
using System.Web.Mvc;
using AllShare.Models;
using AllShare.Services.Account;
using Facebook;
using Microsoft.Practices.Unity;

namespace AllShare.Controllers
{
    public class SettingsController : Controller
    {
        [Dependency]
        public IAccountService AccountService { get; set; }

        public SettingsController(IAccountService accountService)
        {
            AccountService = accountService;
        }

        private Uri RedirectUri
        {
            get
            {
                var uriBuilder = new UriBuilder(Request.Url)
                {
                    Query = null,
                    Fragment = null,
                    Path = Url.Action("FacebookCallback")
                };
                return uriBuilder.Uri;
            }
        }

        // GET: Settings
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Facebook()
        {
            var fb = new FacebookClient();
            var loginUrl = fb.GetLoginUrl(new
            {
                client_id = "197788387246812",
                client_secret = "361cb6f8902601caf3fcfe62e6134cba",
                redirect_uri = RedirectUri.AbsoluteUri,
                response_type = "code",
                scope = "publish_actions"
            });

            return Redirect(loginUrl.AbsoluteUri);
        }

        public ActionResult FacebookCallback(string code)
        {
            var fb = new FacebookClient();
            dynamic result = fb.Post("oauth/access_token", new
            {
                client_id = "197788387246812",
                client_secret = "361cb6f8902601caf3fcfe62e6134cba",
                redirect_uri = RedirectUri.AbsoluteUri,
                code = code
            });

            var accessToken = result.access_token;

            Session["FacebookAccessToken"] = accessToken;
            var user = (AccountViewModel)Session["User"];
            AccountService.SaveFacebookToken(user.Username, accessToken);

            fb.AccessToken = accessToken;

            //long-lived token
            dynamic res = fb.Get("oauth/access_token", new
            {
                grant_type = "fb_exchange_token",
                client_id = "197788387246812",
                client_secret = "361cb6f8902601caf3fcfe62e6134cba",
                fb_exchange_token = accessToken
            });

            return RedirectToAction("Index", "Settings");
        }
    }
}