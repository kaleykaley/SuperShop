using System.ComponentModel.DataAnnotations;

namespace SuperShop.Models
{
    public class ChangePasswordViewModel
    {

        [Required]
        [Display(Name = "Current Password")]
        public string OldPassword { get; set; }

        [Required]
        [Display(Name = "New Password")]
        public string NewPassword { get; set; }


        [Required]
        [Display(Name = "Confirm New Password")]
        [Compare("NewPassword")]
        public string Confirm { get; set; }
    }
}
