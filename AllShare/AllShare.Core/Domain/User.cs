using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AllShare.Core.Domain
{
    public class User
    {   
        [Key]     
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public ICollection<Post> Posts { get; set; }
        public ICollection<SharePostJobModel> Jobs { get; set; }
        public string FacebookToken { get; set; }
        public string TwitterToken { get; set; }
        public string TwitterTokenSecret { get; set; }
        public string Email { get; set; }
    }
}
