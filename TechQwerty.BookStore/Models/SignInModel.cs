using System.ComponentModel.DataAnnotations;

namespace TechQwerty.BookStore.Models
{
    public class SignInModel
    {
        [Required(ErrorMessage = "Please enter your email")]
        [Display(Name = "Email Address")]
        public string Email { get; set; } = string.Empty;
        [Required(ErrorMessage = "Please enter your password")]
        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;

        public bool RememberMe { get; set; }
        
    }
}
