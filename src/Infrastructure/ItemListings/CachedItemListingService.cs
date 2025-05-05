using Application.DTOs;
using Application.Interfaces;
using Application.Mappers;
using Infrastructure.Common;

namespace Infrastructure.ItemListings;

public class CachedItemListingService : ICachedItemListingService
{
    private readonly IItemListingRepository _repository;
    private readonly RedisCacheService _cache;
    private const string CacheKey = "item_listings_all";

    public CachedItemListingService(IItemListingRepository repository, RedisCacheService cache)
    {
        _repository = repository;
        _cache = cache;
    }

    public async Task<IEnumerable<ItemListingDto>> GetAllAsync()
    {
        var cached = await _cache.GetAsync<IEnumerable<ItemListingDto>>(CacheKey);
        if (cached is not null) return cached;

        var listings = await _repository.GetAllAsync();
        var dtos = listings.Select(ItemListingMapper.ToDto).ToList();
        await _cache.SetAsync(CacheKey, dtos, TimeSpan.FromMinutes(5));
        return dtos;
    }

    public async Task<ItemListingDto?> GetByIdAsync(Guid id)
    {
        var listing = await _repository.GetByIdAsync(id);
        return listing is null ? null : ItemListingMapper.ToDto(listing);
    }

    public async Task CreateAsync(ItemListingDto dto)
    {
        var entity = ItemListingMapper.ToEntity(dto);
        await _repository.CreateAsync(entity);
        await _cache.RemoveAsync(CacheKey);
    }
}