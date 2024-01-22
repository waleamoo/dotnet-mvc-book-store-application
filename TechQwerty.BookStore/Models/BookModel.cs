using System.ComponentModel.DataAnnotations;

namespace TechQwerty.BookStore.Models
{
    public class BookModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter the title of your book")]
        [StringLength(100, MinimumLength = 5)]
        public string Title { get; set; } = string.Empty;
        
        [Required(ErrorMessage = "Please enter the author name")]
        public string Author { get; set; } = string.Empty;
        
        [StringLength(500, MinimumLength = 30, ErrorMessage = "Please enter a minimum of 30 characters for description, not more than 500.")]
        public string Description { get; set; } = string.Empty;
        
        public string Category { get; set; } = string.Empty;

        //[Required(ErrorMessage = "Please choose language of the book")]
        [Display(Name = "Book Language")]
        public int LanguageId { get; set; }

        public string? Language { get; set; } = string.Empty;
        
        [Required(ErrorMessage = "Please enter the number of pages of the book")]
        [Display(Name = "Total pages of book")]
        public int? TotalPages { get; set; }

        [Display(Name = "Choose the cover photo of your book")]
        [Required]
        public IFormFile CoverPhoto { get; set; }
        public string CoverImageUrl { get; set; }

        [Display(Name = "Choose the gallery images of your book")]
        [Required]
        public IFormFileCollection GalleryFiles { get; set; }

        public List<GalleryImageModel> Gallery { get; set; }

        [Display(Name = "Upload your book in PDF format")]
        [Required]
        public IFormFile BookPdf { get; set; }
        public string BookPdfUrl { get; set; }
    }
}
