using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Infrastructure.Data.Models;

public class OrderDbModel
{
    [BsonId]
    [BsonRepresentation(BsonType.String)]
    public string Id { get; set; } = Guid.NewGuid().ToString();
    
    [BsonRepresentation(BsonType.String)]
    public string ItemId { get; set; } = string.Empty;
    
    [BsonRepresentation(BsonType.String)]
    public string BuyerId { get; set; } = string.Empty;
    
    public int Quantity { get; set; }
    public decimal TotalPrice { get; set; }
    public DateTime PlacedAt { get; set; } = DateTime.UtcNow;
}