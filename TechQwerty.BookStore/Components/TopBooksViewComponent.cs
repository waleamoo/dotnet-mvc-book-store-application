using Microsoft.AspNetCore.Mvc;
using System.Runtime.InteropServices;
using TechQwerty.BookStore.Repository;

namespace TechQwerty.BookStore.Components
{
    public class TopBooksViewComponent : ViewComponent
    {
        private readonly IBookRepository _bookRepository;

        public TopBooksViewComponent(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var books = await _bookRepository.GetTopBooksAsync();
            return View(books);
        }
    }
}
