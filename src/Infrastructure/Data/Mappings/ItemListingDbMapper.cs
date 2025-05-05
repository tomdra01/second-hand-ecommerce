using Domain.Entities;
using Infrastructure.Data.Models;

namespace Infrastructure.Data.Mappings;

public static class ItemListingDbMapper
{
    public static ItemListingDbModel ToDb(ItemListing entity) => new()
    {
        Id = entity.Id.ToString(),
        Title = entity.Title,
        Description = entity.Description,
        Price = entity.Price,
        SellerId = entity.SellerId,
        ImageUrls = entity.ImageUrls,
        CreatedAt = DateTime.UtcNow
    };

    public static ItemListing ToDomain(ItemListingDbModel dbModel) => new()
    {
        Id = Guid.Parse(dbModel.Id),
        Title = dbModel.Title,
        Description = dbModel.Description,
        Price = dbModel.Price,
        SellerId = dbModel.SellerId,
        ImageUrls = dbModel.ImageUrls
    };
}