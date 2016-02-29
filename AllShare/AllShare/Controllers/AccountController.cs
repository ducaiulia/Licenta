using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using AllShare.Models;
using AllShare.Models.Builders;
using AllShare.Services.Account;
using Facebook;
using Microsoft.Practices.Unity;
using Action = AllShare.Services.Utils.Action;

namespace AllShare.Controllers
{
    public class AccountController : Controller
    {
        #region Dependencies
        [Dependency]
        public IAccountService AccountService { get; set; }
        #endregion

        public AccountController(IAccountService accountService)
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

        // GET: Account
        public ActionResult Index()
        {
            return View();
        }

        #region Facebook Methods
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

            fb.AccessToken = accessToken;

            //dynamic me = fb.Get("me?fields=first_name,last_name,id,email");
            //string email = me.email;
            //FormsAuthentication.SetAuthCookie(email, false);

            //long-lived token
            dynamic res = fb.Get("oauth/access_token", new
            {
                grant_type = "fb_exchange_token",
                client_id = "197788387246812",
                client_secret = "361cb6f8902601caf3fcfe62e6134cba",
                fb_exchange_token = accessToken
            });

            return RedirectToAction("Contact", "Home");
        }

        public ActionResult FacebookPost()
        {
            //await this.loginButton.RequestNewPermissions("publish_stream");

            //var facebookClient = new Facebook.FacebookClient(Session["FacebookAccessToken"].ToString());

            //var postParams = new
            //{
            //    name = "test",
            //    caption = "test",
            //    description = "test",
            //    link = "http://test.test/"
            //};

            //try
            //{
            //    dynamic fbPostTaskResult = await facebookClient.PostTaskAsync("/me/feed", postParams);
            //    var result = (IDictionary<string, object>)fbPostTaskResult;

            //    //var successMessageDialog = new Windows.UI.Popups.MessageDialog("Posted Open Graph Action, id: " + (string)result["id"]);
            //    //await successMessageDialog.ShowAsync();
            //}
            //catch (Exception ex)
            //{
            //    //var exceptionMessageDialog = new Windows.UI.Popups.MessageDialog("Exception during post: " + ex.Message);
            //    //exceptionMessageDialog.ShowAsync();
            //}
            return null;
        }
        #endregion
        
        #region Account Actions

        [HttpPost]
        public ActionResult Login(AccountInput account)
        {
            account.Action = Action.Login;
            var viewModel = new AccountViewModelBuilder(AccountService, account).Build();

            if (viewModel != null)
                Session["User"] = viewModel;
                return Redirect(Url.Action("Index", "Home"));
        }

        [HttpPost]
        public ActionResult Register(AccountInput account)
        {
            account.Action = Action.Register;
            var viewModel = new AccountViewModelBuilder(AccountService, account).Build();
            if (viewModel != null)
                Session["User"] = viewModel;
                return Redirect(Url.Action("Index", "Home"));
        }

        public ActionResult Logout()
        {
            Session["User"] = null;
            return RedirectToAction("Index", "Account");
        }

        #endregion
    }
}