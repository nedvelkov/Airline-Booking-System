using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

using static ABS_DataConstants.DataConstrain;
using static ABS_DataConstants.Error;

namespace ABS_WebApp.ViewModels
{
    public class ResetPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email adress:")]
        public string Email { get; set; }

        [Required]
        [RegularExpression(EVALUATE_PASSWORD, ErrorMessage = PASSWORD_TOOLTIP)]
        [Display(Name = "Password:")]
        public string Password { get; set; }

        [Required]
        [RegularExpression(EVALUATE_PASSWORD, ErrorMessage = PASSWORD_TOOLTIP)]
        [Compare(nameof(Password), ErrorMessage = "Password do not match")]
        [Display(Name = "Confirm password:")]
        public string ConfirmPassword { get; set; }
    }
}
