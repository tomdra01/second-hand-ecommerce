using Application.DTOs;
using Application.Interfaces;
using Application.Mappers;

namespace Application.Queries.GetItemListings;

public class GetAllItemListingHandler
{
    private readonly IItemListingRepository _repository;
    private readonly ICacheService _cache;
    private const string CacheKey = "item_listings_all";

    public GetAllItemListingHandler(IItemListingRepository repository, ICacheService cache)
    {
        _repository = repository;
        _cache = cache;
    }

    public async Task<IEnumerable<ItemListingDto>> HandleAsync(GetAllItemListingQuery query)
    {
        var cached = await _cache.GetAsync<IEnumerable<ItemListingDto>>(CacheKey);
        if (cached is not null) return cached;

        var listings = await _repository.GetAllAsync();
        var dtos = listings.Select(ItemListingMapper.ToDto).ToList();

        await _cache.SetAsync(CacheKey, dtos, TimeSpan.FromMinutes(5));
        return dtos;
    }
}