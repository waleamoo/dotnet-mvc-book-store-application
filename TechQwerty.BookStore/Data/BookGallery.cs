namespace TechQwerty.BookStore.Data
{
    public class BookGallery
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string URL { get; set; } = string.Empty;
        public Book Book { get; set; }

    }
}
