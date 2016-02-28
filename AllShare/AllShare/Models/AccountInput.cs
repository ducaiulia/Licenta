using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Action = AllShare.Services.Utils.Action;

namespace AllShare.Models
{
    public class AccountInput
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public Action Action { get; set; }
    }
}