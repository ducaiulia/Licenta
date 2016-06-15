using System.ComponentModel.DataAnnotations;
using AllShare.Services.Utils;

namespace AllShare.Models
{
    public class AccountInput
    {
        [Required(ErrorMessage = "Username is required.")]
        [StringLength(25, ErrorMessage = "Username must be between 5 and 25 characters", MinimumLength = 5)]
        public string Username { get; set; }
        [Required(ErrorMessage = "Password is required.")]
        [StringLength(25, ErrorMessage = "Password must be between 5 and 25 characters", MinimumLength = 5)]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress]
        public string Email { get; set; }
        public Action Action { get; set; }
    }
}