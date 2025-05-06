using Application.DTOs;
using Application.Interfaces;
using Application.Mappers;

namespace Application.Services;

public class ItemListingService : IItemListingService
{
    private readonly IItemListingRepository _repository;

    public ItemListingService(IItemListingRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<ItemListingDto>> GetAllAsync()
    {
        var listings = await _repository.GetAllAsync();
        return listings.Select(ItemListingMapper.ToDto);
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
        
        dto.Id = entity.Id.ToString();
    }
}