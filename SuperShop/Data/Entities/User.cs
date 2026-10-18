using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace SuperShop.Data.Entities
{
    // identityuser - class from the identity framework that has all the properties for user management,
    // like username, password, email, etc
    public class User : IdentityUser
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }


        [Display(Name = "FullName")]
        public string FullName => $"{FirstName} {LastName}";


    }
}
