using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using AllShare.Models;
using AllShare.Models.Builders;
using AllShare.Services.Account;
using Facebook;
using Microsoft.Practices.Unity;
using Newtonsoft.Json;
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

        // GET: Account
        public ActionResult Index()
        {
            if(Session["User"] != null)
                return Redirect(Url.Action("Index", "Home"));
            return View();
        }
        
        #region Account Methods

        [HttpPost]
        public async Task<ActionResult> Login(AccountInput account)
        {
            account.Action = Action.Login;
            var viewModel = await new AccountViewModelBuilder(AccountService, account).Build();

            if (viewModel != null)
            {
                Session["User"] = viewModel;
                Session["FacebookAccessToken"] = viewModel.FacebookToken;
                Session["TwitterAccessToken"] = viewModel.TwitterToken;
                Session["TwitterAccessTokenSecret"] = viewModel.TwitterTokenSecret;
                return Json(new {url = Url.Action("Index", "Home"), status = 1});
            }
            else
            {
                return Json(new {message = "Your login attempt was not succesful.", status = 0});
            }
        }

        [HttpPost]
        public async Task<ActionResult> Register(AccountInput account)
        {
            account.Action = Action.Register;
            var viewModel = await new AccountViewModelBuilder(AccountService, account).Build();
            if (viewModel != null)
            {
                Session["User"] = viewModel;
                return Json(new { url = Url.Action("Index", "Home"), status = 1 });
            }
            else
            {
                return Json(new { message = "Your register attempt was not succesful.", status = 0 });
            }
        }

        public async Task<ActionResult> Logout()
        {
            var user = (AccountViewModel)Session["User"];
            if (user != null)
            {
                Session["User"] = null;
                await AccountService.Logout(user.Username);
            }
            return RedirectToAction("Index", "Account");
        }

        #endregion
    }
}