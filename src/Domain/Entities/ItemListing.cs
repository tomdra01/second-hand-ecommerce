namespace Domain.Entities;

public class ItemListing
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public string SellerId { get; set; } = string.Empty;
    public List<string> ImageUrls { get; set; } = [];
}