using Application.Commands.CreateItemListing;
using Application.DTOs;

namespace Application.Interfaces;

public interface IItemListingService
{
    Task<IEnumerable<ItemListingDto>> GetAllAsync();
    Task<ItemListingDto?> GetByIdAsync(Guid id);
    Task<string> CreateWithImageAsync(CreateItemListingCommand command);
}