using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AllShare.Models
{
    public class PostViewModel
    {
        public string Text { get; set; }
        public DateTime DateTime { get; set; }
        public AccountViewModel User { get; set; }
    }
}