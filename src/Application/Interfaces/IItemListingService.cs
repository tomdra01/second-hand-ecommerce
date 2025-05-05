using Application.DTOs;

namespace Application.Interfaces;

public interface IItemListingService
{
    Task<IEnumerable<ItemListingDto>> GetAllAsync();
    Task<ItemListingDto?> GetByIdAsync(Guid id);
    Task CreateAsync(ItemListingDto item);
}