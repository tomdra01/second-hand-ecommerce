using Infrastructure.Data.Models;
using MongoDB.Driver;

namespace Infrastructure.Data;

public class MongoDbContext
{
    private readonly IMongoDatabase _database;
    
    public MongoDbContext(string connectionString, string dbName)
    {
        var client = new MongoClient(connectionString);
        _database = client.GetDatabase(dbName);
    }

    public IMongoCollection<ItemListingDbModel> ItemListings => _database.GetCollection<ItemListingDbModel>("ItemListings");
}