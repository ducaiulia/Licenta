using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
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
                return Redirect(Url.Action("Index", "Home"));
            }
            return null;
        }

        [HttpPost]
        public async Task<ActionResult> Register(AccountInput account)
        {
            account.Action = Action.Register;
            var viewModel = await new AccountViewModelBuilder(AccountService, account).Build();
            if (viewModel != null)
                Session["User"] = viewModel;
                return Redirect(Url.Action("Index", "Home"));
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