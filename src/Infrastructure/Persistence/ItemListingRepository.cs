using Application.Interfaces;
using Domain.Entities;
using MongoDB.Driver;

namespace Infrastructure.Persistence;

public class ItemListingRepository : IItemListingRepository
{
    private readonly MongoDbContext _context;

    public ItemListingRepository(MongoDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<ItemListing>> GetAllAsync()
    {
        return await _context.ItemListings.Find(_ => true).ToListAsync();
    }

    public async Task<ItemListing?> GetByIdAsync(Guid id)
    {
        return await _context.ItemListings.Find(x => x.Id == id).FirstOrDefaultAsync();
    }

    public async Task CreateAsync(ItemListing item)
    {
        await _context.ItemListings.InsertOneAsync(item);
    }
}