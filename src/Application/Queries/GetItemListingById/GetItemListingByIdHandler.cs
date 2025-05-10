using Application.Data.DTOs;
using Application.Data.Mappers;
using Application.Interfaces;

namespace Application.Queries.GetItemListingById;

public class GetItemListingByIdHandler
{
    private readonly IItemListingRepository _repository;

    public GetItemListingByIdHandler(IItemListingRepository repository)
    {
        _repository = repository;
    }

    public async Task<ItemListingDto?> HandleAsync(GetItemListingByIdQuery query)
    {
        var listing = await _repository.GetByIdAsync(query.Id);
        return listing is null ? null : ItemListingMapper.ToDto(listing);
    }
}