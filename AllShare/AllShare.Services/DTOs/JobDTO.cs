using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllShare.Services.DTOs
{
    public class JobDTO
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public string AuthorUsername { get; set; }
        public string ImagePath { get; set; }
        public DateTime ToBeRunAt { get; set; }
        public bool IsTwitter { get; set; }
        public bool IsFacebook { get; set; }
    }
}
