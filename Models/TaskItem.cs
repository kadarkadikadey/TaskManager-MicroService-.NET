using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;

namespace TaskManager.Models;
[BsonIgnoreExtraElements]
public class TaskItem
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }

    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
    public string Status { get; set; } = "Pending";
    public DateTime CreatedAt { get; set; } // Must be here
    public DateTime? DueDate { get; set; }  // Must be here
}