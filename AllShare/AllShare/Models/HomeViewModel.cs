using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AllShare.Models
{
    public class HomeViewModel
    {
        public AccountViewModel Account { get; set; }
        public NewsFeedViewModel NewsFeed { get; set; }
        public OnlineUsersViewModel OnlineUsers { get; set; }
        public string Today { get; set; }
    }
}