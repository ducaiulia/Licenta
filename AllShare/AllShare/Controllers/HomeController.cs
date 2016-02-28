using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AllShare.Models;
using AllShare.Models.Builders;
using AllShare.Services.Account;
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
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}