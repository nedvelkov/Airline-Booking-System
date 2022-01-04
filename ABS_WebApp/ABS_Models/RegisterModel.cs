﻿using System.ComponentModel.DataAnnotations;

using static ABS_DataConstants.DataConstrain;
using static ABS_DataConstants.Error;

namespace ABS_Models
{
    public class RegisterModel
    {
        [Required]
        [RegularExpression(EVALUATE_USERNAME, ErrorMessage = USERNAME_TOOLTIP)]
        [Display(Name = "First name:")]
        public string FirstName { get; set; }

        [Required]
        [RegularExpression(EVALUATE_USERNAME, ErrorMessage = USERNAME_TOOLTIP)]
        [Display(Name = "Last name:")]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email adress:")]
        public string Email { get; set; }

        [Required]
        [RegularExpression(EVALUATE_PASSWORD, ErrorMessage = PASSWORD_TOOLTIP)]
        [Display(Name = "Password:")]
        public string Password { get; set; }

        [Required]
        public int Role { get; set; } = 2;
    }
}
