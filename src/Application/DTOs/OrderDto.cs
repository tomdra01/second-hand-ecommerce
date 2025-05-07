namespace Application.DTOs;

public class OrderDto
{
    public string ItemId { get; set; } = string.Empty;
    public string BuyerId { get; set; } = string.Empty;
    public int Quantity { get; set; }
    public decimal TotalPrice { get; set; }
}