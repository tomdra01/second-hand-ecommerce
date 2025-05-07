using API.Requests;
using Application.DTOs;

namespace API.Mappers;

public static class CreateOrderRequestMapper
{
    public static OrderDto ToDto(CreateOrderRequest request) => new()
    {
        ItemId = request.ItemId,
        BuyerId = request.BuyerId,
        Quantity = request.Quantity,
        TotalPrice = request.TotalPrice
    };
}