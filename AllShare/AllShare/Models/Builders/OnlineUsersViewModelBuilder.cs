using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using AllShare.Services.User;
using AllShare.Services.Utils;

namespace AllShare.Models.Builders
{
    public class OnlineUsersViewModelBuilder: IViewModelBuilder<OnlineUsersViewModel>
    {
        private IUserService UserService { get; set; }
        private AccountViewModel CurrentUser { get; set; }

        public OnlineUsersViewModelBuilder(IUserService userService, AccountViewModel currentUser)
        {
            UserService = userService;
            CurrentUser = currentUser;
        }

        public async Task<OnlineUsersViewModel> Build()
        {
            var usernames = new OnlineUsersViewModel {Users = new List<string>()};
            var result = await UserService.GetOnlineUsers();
            if (result.ResultType == ResultType.Success)
                result.Result.ForEach(u =>
                {
                    if (!u.Username.Equals(CurrentUser.Username))
                        usernames.Users.Add(u.Username);

                });

            return usernames;
        }
    }
}