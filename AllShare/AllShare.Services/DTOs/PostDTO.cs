using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AllShare.Core.Domain;

namespace AllShare.Services.DTOs
{
    public class PostDTO
    {
        public string Text { get; set; }
        public DateTime DateTime { get; set; }
        public UserDTO User { get; set; }
        public bool IsFile { get; set; }
        public string ImagePath { get; set; }
    }
}
