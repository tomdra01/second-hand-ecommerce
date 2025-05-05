namespace Application.DTOs;

public class ItemListingDto
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public string SellerId { get; set; } = string.Empty;
}