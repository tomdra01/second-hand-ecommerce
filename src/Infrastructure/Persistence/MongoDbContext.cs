using Domain.Entities;
using MongoDB.Driver;

namespace Infrastructure.Persistence;

public class MongoDbContext
{
    private readonly IMongoDatabase _database;
    
    public MongoDbContext(string connectionString, string dbName)
    {
        var client = new MongoClient(connectionString);
        _database = client.GetDatabase(dbName);
    }

    public IMongoCollection<ItemListing> ItemListings => _database.GetCollection<ItemListing>("ItemListings");
}