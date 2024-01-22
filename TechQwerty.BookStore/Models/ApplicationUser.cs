using Microsoft.AspNetCore.Identity;

namespace TechQwerty.BookStore.Models
{
    public class ApplicationUser : IdentityUser
    {
        // add more columns 
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public DateTime DateOfBirth { get; set; } 
    }
}
