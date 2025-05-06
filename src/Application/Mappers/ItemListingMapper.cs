using Application.DTOs;
using Domain.Entities;

namespace Application.Mappers;

public static class ItemListingMapper
{
    public static ItemListingDto ToDto(ItemListing entity) => new()
    {
        Id = entity.Id.ToString(),
        Title = entity.Title,
        Description = entity.Description,
        Price = entity.Price,
        SellerId = entity.SellerId,
        ImageUrls = entity.ImageUrls 
    };

    public static ItemListing ToEntity(ItemListingDto dto) => new()
    {
        Id = Guid.Empty,
        Title = dto.Title,
        Description = dto.Description,
        Price = dto.Price,
        SellerId = dto.SellerId,
        ImageUrls = dto.ImageUrls
    };
}