using Application.DTOs;
using Application.Interfaces;
using Application.Mappers;

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