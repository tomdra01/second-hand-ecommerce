using API.Requests;
using Application.Commands.CreateOrder;

namespace API.Mappers;

public static class CreateOrderRequestMapper
{
    public static CreateOrderCommand ToCommand(CreateOrderRequest request)
    {
        return new CreateOrderCommand
        {
            ItemId = request.ItemId,
            BuyerId = request.BuyerId,
        };
    }
}