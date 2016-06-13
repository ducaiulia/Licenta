using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AllShare.Core.Domain
{
    public class SharePostJobModel
    {
        [Key]
        public int Id { get; set; }
        public string Text { get; set; }
        public int CurrentUserId { get; set; }
        public string AuthorUsername { get; set; }
        public string ImagePath { get; set; }
        public DateTime ToBeRunAt { get; set; }
        public bool IsTwitter { get; set; }
        public bool IsFacebook { get; set; }
        public bool Finished { get; set; }
        public string FacebookToken { get; set; }
        public string TwitterAccesToken { get; set; }
        public string TwitterSecretToken { get; set; }
    }
}
