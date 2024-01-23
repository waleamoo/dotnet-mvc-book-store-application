namespace TechQwerty.BookStore.Models
{
    public class EmailConfirmModel
    {
        public string Email { get; set; } = string.Empty;
        public bool IsConfirmed { get; set; }
        public bool IsEmailSent { get; set; }
        public bool IsEmailVerified { get; set; }
    }
}
