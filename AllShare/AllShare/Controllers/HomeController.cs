using System;
using System.Threading.Tasks;
using System.Web.Mvc;
using AllShare.Models;
using AllShare.Models.Builders;
using AllShare.Services.Account;
using AllShare.Services.DTOs;
using AllShare.Services.NewsFeed;
using Microsoft.Practices.Unity;
using Nelibur.ObjectMapper;

namespace AllShare.Controllers
{
    public class HomeController : Controller
    {
        [Dependency]
        public IAccountService AccountService { get; set; }
        [Dependency]
        public INewsFeedService NewsFeedService { get; set; }

        public HomeController(IAccountService accountService, INewsFeedService newsFeedService)
        {
            AccountService = accountService;
            NewsFeedService = newsFeedService;
        }

        public ActionResult Index()
        {
            var viewModel = new HomeViewModel();

            viewModel.Account = (AccountViewModel)Session["User"];
            viewModel.NewsFeed = new FeedViewModelBuilder(NewsFeedService).Build();

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

        [HttpPost]
        public async Task<ActionResult> Post(PostViewModel model)
        {
            model.DateTime = DateTime.Now;
            model.User = (AccountViewModel) Session["User"];

            var dto = TinyMapper.Map<PostDTO>(model);

            NewsFeedService.AddPost(dto);
            return Redirect(Url.Action("Index", "Home"));
        }
    }
}