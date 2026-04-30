using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace TechTrackerBlazor.Models
{
    public class Customer
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [Required]
        [MaxLength(40)]
        [BsonElement("FirstName")]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        [MaxLength(40)]
        [BsonElement("LastName")]
        public string LastName { get; set; } = string.Empty;

        [EmailAddress]
        [BsonElement("Email")]
        public string? Email { get; set; }

        [Required]
        [Phone]
        [BsonElement("Phone")]
        public string Phone { get; set; } = string.Empty;

        [BsonElement("CreationDate")]
        public DateTime CreationDate { get; set; } = DateTime.Today;

        [BsonIgnore]
        public string SearchFirstName { get; set; } = string.Empty;
        
        [BsonIgnore]
        public string SearchLastName { get; set; } = string.Empty;
        
        [BsonIgnore]
        public string SearchEmail { get; set; } = string.Empty;
        
        [BsonIgnore]
        public string SearchPhone { get; set; } = string.Empty;

        ///indexes to-be-implemented: FirstName, LastName, Email, Phone  aislopfilledthistext
        ///NOT RESTRICTED TO THESE!!! ADD/CHANGE IF WE NEED MORE OR BETTER ONES
    }
}
