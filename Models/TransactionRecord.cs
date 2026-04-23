using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace TechTrackerBlazor.Models
{
    public class TransactionRecord
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonElement("OrderId")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? OrderId { get; set; }

        [BsonElement("Cost")]
        public decimal Cost { get; set; }

        [BsonElement("PaymentMethod")]
        public string? PaymentMethod { get; set; }

        [BsonElement("Date")]
        public DateTime Date { get; set; }

        [BsonElement("Completed")] 
        public bool Completed { get; set; }
    }
}
