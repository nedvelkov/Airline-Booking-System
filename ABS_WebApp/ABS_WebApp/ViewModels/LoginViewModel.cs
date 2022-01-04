using System.ComponentModel.DataAnnotations;


namespace ABS_WebApp.ViewModels
{
    public class LoginViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email adress:")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Password:")]
        public string Password { get; set; }
    }
}
