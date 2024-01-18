using Microsoft.AspNetCore.Mvc;
using TechQwerty.BookStore.Models;
using TechQwerty.BookStore.Repository;

namespace TechQwerty.BookStore.Controllers
{
    public class BookController : Controller
    {
        private readonly BookRepository _bookRepository = null;

        public BookController()
        {
            _bookRepository = new BookRepository();
        }

        public List<BookModel> Index()
        {
            return _bookRepository.GetAllBooks();
        }

        public ViewResult GetAllBooks()
        {
            var data = _bookRepository.GetAllBooks();
            return View(data);
        }

        public ViewResult GetBook(int id)
        {
            var book = _bookRepository.GetBookById(id);
            return View(book);
        }
    }
}
