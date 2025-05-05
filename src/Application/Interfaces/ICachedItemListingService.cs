using Application.DTOs;

namespace Application.Interfaces;

public interface ICachedItemListingService
{
    Task<IEnumerable<ItemListingDto>> GetAllAsync();
    Task<ItemListingDto?> GetByIdAsync(Guid id);
    Task CreateAsync(ItemListingDto dto);
}