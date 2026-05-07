using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace TechTrackerBlazor.Models
{
    public class Address
    {
        public string? Street { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? ZipCode { get; set; }
    }
    [BsonIgnoreExtraElements]
    public class Employee
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        public string FirstName { get; set; } = "";
        public string LastName { get; set; } = "";
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; } 
        [BsonElement("address")] 
        public Address EmployeeAddress { get; set; } = new Address();
        public string? PasswordHash { get; set; }
        public string EmployeeRole { get; set; } = "Technician"; 
        public bool isAvailable { get; set; } = true; 
    }
}
