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


        ///indexes to-be-implemented: FirstName, LastName, Email, Phone  aislopfilledthistext
        ///NOT RESTRICTED TO THESE!!! ADD/CHANGE IF WE NEED MORE OR BETTER ONES
        public DateTime CreationDate { get; set; } = DateTime.Today;


    }
}
