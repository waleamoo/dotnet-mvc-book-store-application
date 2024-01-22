using TechQwerty.BookStore.Models;

namespace TechQwerty.BookStore.Repository
{
    public interface ILanguageRepository
    {
        Task<List<LanguageModel>> GetLanguages();
    }
}