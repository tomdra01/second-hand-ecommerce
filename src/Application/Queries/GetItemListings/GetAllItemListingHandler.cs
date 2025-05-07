using Application.DTOs;
using Application.Interfaces;
using Application.Mappers;

namespace Application.Queries.GetItemListings;

public class GetAllItemListingHandler
{
    private readonly IItemListingRepository _repository;

    public GetAllItemListingHandler(IItemListingRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<ItemListingDto>> HandleAsync(GetAllItemListingQuery query)
    {
        var listings = await _repository.GetAllAsync();
        return listings.Select(ItemListingMapper.ToDto);
    }
}