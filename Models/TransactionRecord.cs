using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace TechTrackerBlazor.Models
{
    [BsonIgnoreExtraElements]
    public class TransactionRecord
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonElement("OrderId")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? OrderId { get; set; }

        public decimal Cost { get; set; }
        public string PaymentMethod { get; set; } = "Credit Card";
        public DateTime Date { get; set; } = DateTime.UtcNow;
        public bool Completed { get; set; } = false; 
    }
}
