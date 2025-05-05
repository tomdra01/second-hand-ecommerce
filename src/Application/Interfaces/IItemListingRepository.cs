using Domain.Entities;

namespace Application.Interfaces;

public interface IItemListingRepository
{
    Task<IEnumerable<ItemListing>> GetAllAsync();
    Task<ItemListing?> GetByIdAsync(Guid id);
    Task CreateAsync(ItemListing item);
}