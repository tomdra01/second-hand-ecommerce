using API.Data.Requests;
using Application.Commands.CreateOrder;

namespace API.Data.Mappers;

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