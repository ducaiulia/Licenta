using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using AllShare.App_Start;
using AllShare.Models;
using AllShare.Services.Account;
using Microsoft.Practices.Unity;

namespace AllShare
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            UnityWebActivator.Start();
            MapperConfig.Register();
            //Jobs.JobScheduler.Start();
        }

        protected async void Session_End(object sender, EventArgs e)
        {
            var user = (AccountViewModel) Session["User"];
            Session["User"] = null;
            if (user != null)
            {
                var container = UnityConfig.GetConfiguredContainer();
                var accountService = container.Resolve<IAccountService>();
                await accountService.Logout(user.Username);
            }
        }
    }
}
