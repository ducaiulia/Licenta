using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using AllShare.Models;
using AllShare.Models.Builders;
using AllShare.Services.Account;
using Facebook;
using Microsoft.Practices.Unity;

namespace AllShare.Controllers
{
    public class HomeController : Controller
    {
        [Dependency]
        public IAccountService AccountService { get; set; }

        public HomeController(IAccountService accountService)
        {
            AccountService = accountService;
        }

        public ActionResult Index()
        {
            var viewModel = (AccountViewModel)Session["User"];
            return View(viewModel);
        }

        public ActionResult About()
        {
            return View();
        }

        public async Task<ActionResult> Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}