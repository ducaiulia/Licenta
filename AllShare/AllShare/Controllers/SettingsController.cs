using System;
using System.Threading.Tasks;
using System.Web.Configuration;
using System.Web.Mvc;
using AllShare.Models;
using AllShare.Services.Account;
using AllShare.Services.Social;
using AllShare.Services.Utils;
using Facebook;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.StaticFactory;
using TweetSharp;

namespace AllShare.Controllers
{
    public class SettingsController : Controller
    {
        [Dependency]
        public IAccountService AccountService { get; set; }
        [Dependency]
        public ISocialService SocialService { get; set; }

        private static readonly string ConsumerKey = WebConfigurationManager.AppSettings["TwitterConsumerKey"];
        private static readonly string ConsumerSecret = WebConfigurationManager.AppSettings["TwitterConsumerSecret"];
        private static readonly string DomainName = WebConfigurationManager.AppSettings["DomainName"];

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
            if (Session["User"] == null)
                return Redirect(Url.Action("Index", "Account"));

            var user = (AccountViewModel) Session["User"];
            var viewModel = new SettingsViewModel {Jobs = SocialService.GetNotRunJobs(user.UserId)};

            return View(viewModel);
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
            var viewmodel = (AccountViewModel)Session["User"];
            AccountService.SaveFacebookToken(viewmodel.Username, accessToken);

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

        public async Task<ActionResult> Twitter()
        {
            TwitterService service = new TwitterService(ConsumerKey, ConsumerSecret);
            string url = String.Format("http://{0}/{1}", DomainName, Url.Action("TwitterCallback"));
            OAuthRequestToken requestToken = service.GetRequestToken(url);

            Uri uri = service.GetAuthorizationUri(requestToken);
            return new RedirectResult(uri.ToString(), false);
        }

        public async Task<ActionResult> TwitterCallback(string oauth_token, string oauth_verifier)
        {
            var requestToken = new OAuthRequestToken { Token = oauth_token };

            TwitterService service = new TwitterService(ConsumerKey, ConsumerSecret);

            OAuthAccessToken accessToken = service.GetAccessToken(requestToken, oauth_verifier);

            service.AuthenticateWith(accessToken.Token, accessToken.TokenSecret);
            TwitterUser user = service.VerifyCredentials(new VerifyCredentialsOptions());

            Session["TwitterAccessToken"] = accessToken.Token;
            Session["TwitterAccessTokenSecret"] = accessToken.TokenSecret;
            var viewmodel = (AccountViewModel)Session["User"];

            AccountService.SaveTwitterToken(viewmodel.Username, accessToken.Token, accessToken.TokenSecret);

            return RedirectToAction("Index", "Settings");
        }

        public async Task LogOutOnUnload()
        {
            if (Session["User"] != null)
            {
                var user = (AccountViewModel)Session["User"];
                Session["User"] = null;
                await AccountService.Logout(user.Username);
            }
        }
    }
}