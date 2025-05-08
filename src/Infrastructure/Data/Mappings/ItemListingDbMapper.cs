using Domain.Entities;
using Infrastructure.Data.Models;

namespace Infrastructure.Data.Mappings;

public static class ItemListingDbMapper
{
    public static ItemListingDbModel ToDb(ItemListing item) => new()
    {
        Id = item.Id.ToString(),
        Title = item.Title,
        Description = item.Description,
        Price = item.Price,
        SellerId = item.SellerId,
        ImageUrls = item.ImageUrls,
        IsSold = item.IsSold
    };

    public static ItemListing ToDomain(ItemListingDbModel model) => new()
    {
        Id = Guid.Parse(model.Id),
        Title = model.Title,
        Description = model.Description,
        Price = model.Price,
        SellerId = model.SellerId,
        ImageUrls = model.ImageUrls,
        IsSold = model.IsSold
    };
}