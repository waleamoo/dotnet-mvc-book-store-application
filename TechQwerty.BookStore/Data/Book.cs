namespace TechQwerty.BookStore.Data
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Author { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public int LanguageId { get; set; }
        public int TotalPages { get; set; }
        public string CoverImageUrl { get; set; } = string.Empty;
        public DateTime? CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }

        public Language Language { get; set; }

        public ICollection<BookGallery> BookGallery { get; set; }

    }
}
