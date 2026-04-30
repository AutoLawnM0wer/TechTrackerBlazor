using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace TechTrackerBlazor.Models
{
    public class InventoryItem
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        public string PartName { get; set; } = "";
        public string? PartLocation { get; set; }
        
        public int Stock { get; set; }
        public int LowStockThreshold { get; set; }
        public decimal Cost { get; set; }

        [BsonElement("CompatabilityTag")]
        public string? CompatibilityTag { get; set; }

        [BsonElement("supplier")]
        public string? Supplier { get; set; }
    }
}
