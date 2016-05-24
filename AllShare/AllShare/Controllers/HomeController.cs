using System;
using System.Threading.Tasks;
using System.Web.Mvc;
using AllShare.Models;
using AllShare.Models.Builders;
using AllShare.Services.Account;
using AllShare.Services.DTOs;
using AllShare.Services.NewsFeed;
using AllShare.Services.User;
using HandlebarsDotNet;
using Microsoft.Practices.Unity;
using Nelibur.ObjectMapper;
using Newtonsoft.Json;

namespace AllShare.Controllers
{
    public class HomeController : Controller
    {
        [Dependency]
        public IAccountService AccountService { get; set; }
        [Dependency]
        public INewsFeedService NewsFeedService { get; set; }
        [Dependency]
        public IUserService UserService { get; set; }

        public HomeController(IAccountService accountService, INewsFeedService newsFeedService, IUserService userService)
        {
            AccountService = accountService;
            NewsFeedService = newsFeedService;
            UserService = userService;
        }

        public async Task<ActionResult> Index()
        {
            if (Session["User"] == null)
                return Redirect(Url.Action("Index", "Account"));

            var viewModel = new HomeViewModel();

            viewModel.Account = (AccountViewModel)Session["User"];
            viewModel.NewsFeed = await new FeedViewModelBuilder(NewsFeedService).Build();
            viewModel.OnlineUsers = await new OnlineUsersViewModelBuilder(UserService, viewModel.Account).Build();

            return View(viewModel);
        }

        [HttpPost]
        public async Task<string> GetOnlineUsers()
        {
            string source = "{{#if Users}}{{#each Users}}<small>{{this}}</small><br/>{{/each}}{{else}}<small>No online users.</small>{{/if}}";

            var template = Handlebars.Compile(source);
            
            var onlineUsers = await new OnlineUsersViewModelBuilder(UserService, (AccountViewModel)Session["User"]).Build();
            
            var json = JsonConvert.SerializeObject(template(onlineUsers));

            return json.Replace('\"', ' ');
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