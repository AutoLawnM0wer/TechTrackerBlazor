using System.ComponentModel.DataAnnotations;

namespace TechTrackerBlazor.Models
{
    public class Customer
    {
        public string Id { get; set; } = Guid.NewGuid().ToString("N");

        [Required]
        [MaxLength(40)]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        [MaxLength(40)]
        public string LastName { get; set; } = string.Empty;

        [EmailAddress]
        public string? Email { get; set; }

        [Required]
        [Phone]
        public string Phone { get; set; } = string.Empty;

        public DateTime CreationDate { get; set; } = DateTime.Today;

        public string SearchFirstName { get; set; } = string.Empty;

        public string SearchLastName { get; set; } = string.Empty;

        public string SearchEmail { get; set; } = string.Empty;

        public string SearchPhone { get; set; } = string.Empty;
    }
}
