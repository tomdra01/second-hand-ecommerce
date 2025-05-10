using Application.Data.DTOs;
using Domain.Entities;

namespace Application.Data.Mappers;

public static class OrderMapper
{
    public static OrderDto ToDto(Order entity) => new()
    {
        ItemId = entity.ItemId,
        BuyerId = entity.BuyerId,
        TotalPrice = entity.TotalPrice
    };

    public static Order ToEntity(OrderDto dto) => new()
    {
        Id = Guid.NewGuid(),
        ItemId = dto.ItemId,
        BuyerId = dto.BuyerId,
        TotalPrice = dto.TotalPrice,
        PlacedAt = DateTime.UtcNow
    };
}