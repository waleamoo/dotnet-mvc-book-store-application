using Microsoft.AspNetCore.Mvc.TagHelpers;
using TechQwerty.BookStore.Models;

namespace TechQwerty.BookStore.Repository
{
    public class BookRepository
    {

        public List<BookModel> GetAllBooks()
        {
            var books = Books();
            return books;
        }

        public BookModel GetBookById(int id)
        {
            var books = Books();
            return books.FirstOrDefault(x => x.Id == id);
        }

        public List<BookModel> SearchBook(string title, string authorName)
        {
            return new List<BookModel> { new BookModel() };
        }

        private List<BookModel> Books()
        {
            return new List<BookModel>
            {
                new BookModel() {Id = 1, Title = "Programming in PHP", Author = "Murach", Description = "Everything about programming in PHP", Category = "Programming", Language = "English", TotalPages = 1067 },
                new BookModel() {Id = 2, Title = "Programming in PHP", Author = "Murach", Description = "Everything about programming in PHP", Category = "Programming", Language = "English", TotalPages = 1067 },
                new BookModel() {Id = 3, Title = "Programming in PHP", Author = "Murach", Description = "Everything about programming in PHP", Category = "Programming", Language = "English", TotalPages = 1067 },
                new BookModel() {Id = 4, Title = "Programming in PHP", Author = "Murach", Description = "Everything about programming in PHP", Category = "Programming", Language = "English", TotalPages = 1067 },
                new BookModel() {Id = 5, Title = "Programming in PHP", Author = "Murach", Description = "Everything about programming in PHP", Category = "Programming", Language = "English", TotalPages = 1067 },
                new BookModel() {Id = 6, Title = "Programming in PHP", Author = "Murach", Description = "Everything about programming in PHP", Category = "Programming", Language = "English", TotalPages = 1067}
            };
        }
    }
}
