using Domain.Entities;
using Infrastructure.Data.Models;

namespace Infrastructure.Data.Mappings;

public static class OrderDbMapper
{
    public static OrderDbModel ToDb(Order entity) => new()
    {
        Id = entity.Id == Guid.Empty ? Guid.NewGuid().ToString() : entity.Id.ToString(),
        ItemId = entity.ItemId,
        BuyerId = entity.BuyerId,
        TotalPrice = entity.TotalPrice,
        PlacedAt = entity.PlacedAt
    };

    public static Order ToDomain(OrderDbModel dbModel) => new()
    {
        Id = Guid.Parse(dbModel.Id),
        ItemId = dbModel.ItemId,
        BuyerId = dbModel.BuyerId,
        TotalPrice = dbModel.TotalPrice,
        PlacedAt = dbModel.PlacedAt
    };
}