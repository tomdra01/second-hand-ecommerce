namespace API.Requests;

public class CreateOrderRequest
{
    public string ItemId { get; set; } = string.Empty;
    public string BuyerId { get; set; } = string.Empty;
    public decimal TotalPrice { get; set; }
}