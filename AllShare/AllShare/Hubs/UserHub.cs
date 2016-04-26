using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace AllShare.Hubs
{
    public class UserHub : Hub
    {
        public void UpdateOnlineUsers()
        {
            Clients.AllExcept(new []{Context.ConnectionId}).updateOnlineUsersMessage();
        }
    }
}