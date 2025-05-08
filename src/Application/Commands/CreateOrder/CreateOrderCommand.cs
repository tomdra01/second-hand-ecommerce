namespace Application.Commands.CreateOrder;

public class CreateOrderCommand
{
    public string ItemId { get; set; } = string.Empty;
    public string BuyerId { get; set; } = string.Empty;
}