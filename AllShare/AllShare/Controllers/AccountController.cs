using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.Web;
using System.Web.Mvc;
using AllShare.Models;
using AllShare.Models.Builders;
using AllShare.Services.Account;
using Microsoft.Practices.Unity;
using Action = AllShare.Services.Utils.Action;

namespace AllShare.Controllers
{
    public class AccountController : Controller
    {
        [Dependency]
        public IAccountService AccountService { get; set; }
        public AccountController(IAccountService accountService)
        {
            AccountService = accountService;
        }

        // GET: Account
        public ActionResult Index()
        {
            return View();
        }

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
    }
}