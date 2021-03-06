using System.ComponentModel.DataAnnotations;

namespace ABS_WebApp.ViewModels
{
    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email address:")]
        public string Email { get; set; }
    }
}
