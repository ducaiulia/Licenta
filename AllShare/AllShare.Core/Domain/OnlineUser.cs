using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllShare.Core.Domain
{
    public class OnlineUser
    {
        [Key]
        public string Username { get; set; }
    }
}
