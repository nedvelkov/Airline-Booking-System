﻿using System.ComponentModel.DataAnnotations;
using ABS_Models;

using static ABS_DataConstants.DataConstrain;
using static ABS_DataConstants.Error;

namespace ABS_WebApp.ViewModels
{
    public class RegisterViewModel:RegisterModel
    {
        [Required]
        [RegularExpression(EVALUATE_PASSWORD, ErrorMessage = PASSWORD_TOOLTIP)]
        [Compare(nameof(Password), ErrorMessage = "Password do not match")]
        [Display(Name = "Confirm password:")]
        public string ConfirmPassword { get; set; }
    }
}
