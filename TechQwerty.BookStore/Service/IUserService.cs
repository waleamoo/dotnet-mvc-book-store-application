namespace TechQwerty.BookStore.Service
{
    public interface IUserService
    {
        string GetUserId();
        bool IsAuthenticated();
    }
}