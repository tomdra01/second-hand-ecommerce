using Application.DTOs;
using Domain.Entities;

namespace Application.Mappers;

public static class ItemListingMapper
{
    public static ItemListingDto ToDto(ItemListing entity) => new()
    {
        Title = entity.Title,
        Description = entity.Description,
        Price = entity.Price,
        SellerId = entity.SellerId
    };

    public static ItemListing ToEntity(ItemListingDto dto) => new()
    {
        Id = Guid.NewGuid(),
        Title = dto.Title,
        Description = dto.Description,
        Price = dto.Price,
        SellerId = dto.SellerId
    };
}