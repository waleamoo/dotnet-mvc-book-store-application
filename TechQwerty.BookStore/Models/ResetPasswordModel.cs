using System.ComponentModel.DataAnnotations;

namespace TechQwerty.BookStore.Models
{
    public class ResetPasswordModel
    {
        [Required]
        public string UserId { get; set; } = string.Empty;
        [Required]
        public string Token { get; set; } = string.Empty;
        [Required, DataType(DataType.Password)]
        public string NewPassword { get; set; } = string.Empty;
        [Required, DataType(DataType.Password)]
        [Compare("NewPassword")]
        public string ConfirmNewPassword { get; set; } = string.Empty;
        public bool IsSuccess { get; set; }
    }
}
