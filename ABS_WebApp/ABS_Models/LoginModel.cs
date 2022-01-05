using System.ComponentModel.DataAnnotations;


namespace ABS_Models
{
    public class LoginModel
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
