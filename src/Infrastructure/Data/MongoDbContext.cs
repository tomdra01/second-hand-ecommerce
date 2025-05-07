using Infrastructure.Data.Models;
using MongoDB.Driver;

namespace Infrastructure.Data;

public class MongoDbContext
{
    private readonly IMongoClient _client;
    private readonly IMongoDatabase _database;

    public MongoDbContext(string connectionString, string dbName)
    {
        _client = new MongoClient(connectionString);
        _database = _client.GetDatabase(dbName);
    }

    public IClientSessionHandle StartSession() => _client.StartSession();

    public IMongoCollection<ItemListingDbModel> ItemListings =>
        _database.GetCollection<ItemListingDbModel>("ItemListings");

    public IMongoCollection<OrderDbModel> Orders =>
        _database.GetCollection<OrderDbModel>("Orders");
}