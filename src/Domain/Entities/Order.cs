namespace Domain.Entities;

public class Order
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string ItemId { get; set; } = string.Empty;
    public string BuyerId { get; set; } = string.Empty;
    public decimal TotalPrice { get; set; }
    public DateTime PlacedAt { get; set; } = DateTime.UtcNow;
}