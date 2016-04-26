using System;
using System.ComponentModel.DataAnnotations;

namespace AllShare.Core.Domain
{
    public class Post
    {
        [Key]
        public int Id { get; set; }
        public string Text { get; set; }
        public DateTime DateTime { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
