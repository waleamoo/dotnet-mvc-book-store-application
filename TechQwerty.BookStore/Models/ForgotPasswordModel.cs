using System.ComponentModel.DataAnnotations;

namespace TechQwerty.BookStore.Models
{
    public class ForgotPasswordModel
    {
        [Required, EmailAddress, Display(Name = "Registered email address")]
        public string Email { get; set; } = string.Empty;
        public bool IsEmailSent { get; set; }
    }
}
