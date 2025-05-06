using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Infrastructure.Data.Models;

public class ItemListingDbModel
{
    [BsonId]
    [BsonRepresentation(BsonType.String)]
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public string SellerId { get; set; } = string.Empty;
    public List<string> ImageUrls { get; set; } = new();
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}