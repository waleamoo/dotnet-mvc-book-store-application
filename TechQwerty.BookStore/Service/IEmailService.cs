using TechQwerty.BookStore.Models;

namespace TechQwerty.BookStore.Service
{
    public interface IEmailService
    {
        Task SendTestEmail(UserEmailOptions userEmailOptions);
    }
}