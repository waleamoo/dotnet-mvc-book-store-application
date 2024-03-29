﻿using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.EntityFrameworkCore;
using TechQwerty.BookStore.Data;
using TechQwerty.BookStore.Models;

namespace TechQwerty.BookStore.Repository
{
    public class BookRepository : IBookRepository
    {
        private readonly BookStoreContext _context;

        public BookRepository(BookStoreContext context)
        {
            _context = context;
        }

        public async Task<int> AddNewBook(BookModel model)
        {
            var newBook = new Book()
            {
                Author = model.Author,
                CreatedOn = DateTime.UtcNow,
                Description = model.Description,
                Title = model.Title,
                LanguageId = model.LanguageId,
                TotalPages = model.TotalPages.HasValue ? model.TotalPages.Value : 0,
                UpdatedOn = DateTime.UtcNow,
                CoverImageUrl = model.CoverImageUrl,
                BookPdfUrl = model.BookPdfUrl
            };

            newBook.BookGallery = new List<BookGallery>();

            foreach (var file in model.Gallery)
            {
                newBook.BookGallery.Add(new BookGallery()
                {
                    Name = file.Name,
                    URL = file.URL
                });
            }

            await _context.Books.AddAsync(newBook);
            await _context.SaveChangesAsync();
            return newBook.Id;
        }

        public async Task<List<BookModel>> GetAllBooks()
        {
            List<BookModel> books = new List<BookModel>();
            List<Book> allbooks = await _context.Books.Include(x => x.Language).ToListAsync();
            if (allbooks?.Any() == true)
            {
                foreach (var book in allbooks)
                {
                    books.Add(new BookModel()
                    {
                        Id = book.Id,
                        Author = book.Author,
                        Category = book.Category,
                        Description = book.Description,
                        LanguageId = book.LanguageId,
                        Language = book.Language.Name ?? "",
                        Title = book.Title,
                        TotalPages = book.TotalPages,
                        CoverImageUrl = book.CoverImageUrl
                    });
                }
            }
            return books;
        }

        public async Task<List<BookModel>> GetTopBooksAsync()
        {
            return await _context.Books.
                Select(book => new BookModel()
                {
                    Id = book.Id,
                    Author = book.Author,
                    Category = book.Category,
                    Description = book.Description,
                    LanguageId = book.LanguageId,
                    Language = book.Language.Name ?? "",
                    Title = book.Title,
                    TotalPages = book.TotalPages,
                    CoverImageUrl = book.CoverImageUrl
                }).Take(5).ToListAsync();
        }

        public async Task<BookModel> GetBookById(int id)
        {
            return await _context.Books.Where(x => x.Id == id)
                    .Select(book => new BookModel()
                    {
                        Author = book.Author,
                        Category = book.Category,
                        Description = book.Description,
                        Id = book.Id,
                        LanguageId = book.LanguageId,
                        Language = book.Language.Name,
                        Title = book.Title,
                        TotalPages = book.TotalPages,
                        CoverImageUrl = book.CoverImageUrl,
                        Gallery = book.BookGallery.Select(g => new GalleryImageModel()
                        {
                            Id = g.Id,
                            Name = g.Name,
                            URL = g.URL
                        }).ToList(),
                        BookPdfUrl = book.BookPdfUrl
                    }).FirstOrDefaultAsync();

        }

        public List<BookModel> SearchBook(string title, string authorName)
        {
            return new List<BookModel> { new BookModel() };
        }

        //private List<BookModel> Books()
        //{
        //    return new List<BookModel>
        //    {
        //        new BookModel() {Id = 1, Title = "Programming in PHP", Author = "Murach", Description = "Everything about programming in PHP", Category = "Programming", Language = "English", TotalPages = 1067 },
        //        new BookModel() {Id = 2, Title = "Programming in PHP", Author = "Murach", Description = "Everything about programming in PHP", Category = "Programming", Language = "English", TotalPages = 1067 },
        //        new BookModel() {Id = 3, Title = "Programming in PHP", Author = "Murach", Description = "Everything about programming in PHP", Category = "Programming", Language = "English", TotalPages = 1067 },
        //        new BookModel() {Id = 4, Title = "Programming in PHP", Author = "Murach", Description = "Everything about programming in PHP", Category = "Programming", Language = "English", TotalPages = 1067 },
        //        new BookModel() {Id = 5, Title = "Programming in PHP", Author = "Murach", Description = "Everything about programming in PHP", Category = "Programming", Language = "English", TotalPages = 1067 },
        //        new BookModel() {Id = 6, Title = "Programming in PHP", Author = "Murach", Description = "Everything about programming in PHP", Category = "Programming", Language = "English", TotalPages = 1067}
        //    };
        //}
    }
}
