using System.ComponentModel.DataAnnotations;

using static ABS_DataConstants.DataConstrain;
using static ABS_DataConstants.Error;

namespace ABS_WebApp.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        [Display(Name = "User name:")]
        public string UserName { get; set; }

        [Required]
        [Display(Name = "First name:")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last name:")]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email adress:")]
        public string Email { get; set; }

        [Required]
        [RegularExpression(EVALUATE_PASSWORD,ErrorMessage =PASSWORD_TOOLTIP)]
        [Display(Name = "Password:")]
        public string Password { get; set; }

        [Required]
        [RegularExpression(EVALUATE_PASSWORD, ErrorMessage = PASSWORD_TOOLTIP)]
        [Compare(nameof(Password),ErrorMessage ="Password do not match")]
        [Display(Name = "Confirm password:")]
        public string ConfirmPassword { get; set; }
    }
}
