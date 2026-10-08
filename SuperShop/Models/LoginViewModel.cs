using System.ComponentModel.DataAnnotations;

namespace SuperShop.Models
{
    // class for logins - email and password
    public class LoginViewModel
    {
        [Required]
        [EmailAddress]
        public string Username { get; set; }

        [Required]
        [MinLength(6)]
        public string Password { get; set; }

        // property to remember the user so they don't have to log in again
        public bool RememberMe { get; set; }
    }
}
