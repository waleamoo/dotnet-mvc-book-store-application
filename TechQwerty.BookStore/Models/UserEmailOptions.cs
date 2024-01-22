namespace TechQwerty.BookStore.Models
{
    public class UserEmailOptions
    {
        public List<string> ToEmails { get; set; } = new List<string>();
        public string Subject { get; set; } = string.Empty;
        public string Body { get; set; } = string.Empty;
        public List<KeyValuePair<string, string>> PlaceHolders { get; set; } = new();
    }
}
