using Microsoft.EntityFrameworkCore;
using TechQwerty.BookStore.Models;

namespace TechQwerty.BookStore.Data
{
    public class BookStoreContext : DbContext
    {
        public BookStoreContext(DbContextOptions<BookStoreContext> options): base (options)
        {

        }

        public DbSet<Book> Books { get; set; }
        public DbSet<Language> Languages { get; set; }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer("Server=.;Database=BookStore;Integrated Security=True;");
        //    base.OnConfiguring(optionsBuilder);
        //}
    }
}
