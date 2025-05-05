using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Data;
using Infrastructure.Data.Models;
using Infrastructure.Data.Mappings;
using MongoDB.Driver;

namespace Infrastructure.ItemListings;

public class ItemListingRepository : IItemListingRepository
{
    private readonly IMongoCollection<ItemListingDbModel> _collection;

    public ItemListingRepository(MongoDbContext context)
    {
        _collection = context.ItemListings;
    }

    public async Task<IEnumerable<ItemListing>> GetAllAsync()
    {
        var dbModels = await _collection.Find(_ => true).ToListAsync();
        return dbModels.Select(ItemListingDbMapper.ToDomain);
    }

    public async Task<ItemListing?> GetByIdAsync(Guid id)
    {
        var dbModel = await _collection
            .Find(x => x.Id == id.ToString())
            .FirstOrDefaultAsync();

        return dbModel is null ? null : ItemListingDbMapper.ToDomain(dbModel);
    }

    public async Task CreateAsync(ItemListing item)
    {
        var dbModel = ItemListingDbMapper.ToDb(item);
        await _collection.InsertOneAsync(dbModel);
    }
}