namespace Application.Data.DTOs;

public class OrderDto
{
    public string ItemId { get; set; } = string.Empty;
    public string BuyerId { get; set; } = string.Empty;
    public decimal TotalPrice { get; set; }
}