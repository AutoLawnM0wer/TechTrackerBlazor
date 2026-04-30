using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace TechTrackerBlazor.Models
{
    public class RepairOrder
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        public string? CustomerId { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        public string? DeviceId { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        public List<string> AssignedEmployeeId { get; set; } = new();

        public string Status { get; set; } = "Pending";
        public double HoursBillable { get; set; }
        public string? PartsUsed { get; set; }
        public string? Notes { get; set; }

        public DateTime CreationDate { get; set; } = DateTime.UtcNow;
        public DateTime? UpdateDate { get; set; }
        public DateTime? CompletionDate { get; set; }

        [BsonElement("estimatedCost")]
        public decimal EstimatedCost { get; set; }

        [BsonElement("issueType")]
        public string? IssueType { get; set; }

        [BsonElement("priority")]
        public string Priority { get; set; } = "Normal";
    }
}
