using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Web;

namespace AllShare.Models
{
    public class AccountViewModel
    {
        public string Username { get; set; }
        public bool IsFbAuthenticated { get; set; }
        public bool IsTwitterAuthenticated { get; set; }
        public string FacebookToken { get; set; }
        public string TwitterToken { get; set; }
        public string TwitterTokenSecret { get; set; }
        public int UserId { get; set; }
    }
}