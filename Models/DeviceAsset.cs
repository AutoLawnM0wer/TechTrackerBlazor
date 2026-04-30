using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace TechTrackerBlazor.Models
{
    [BsonIgnoreExtraElements] 
    public class DeviceAsset
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        public string? CustomerId { get; set; }
        public string DeviceType { get; set; } = "Smartphone";
        public string Brand { get; set; } = "";
        public string Model { get; set; } = "";
        public string? SerialNumber { get; set; }
        public string? ConditionNotes { get; set; }

        [BsonElement("underWarranty")]
        public bool UnderWarranty { get; set; } 
        public DateTime DateAdded { get; set; } = DateTime.UtcNow;
    }
}
