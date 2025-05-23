using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Data;
using Infrastructure.Data.Mappings;
using Infrastructure.Data.Models;
using MongoDB.Driver;

namespace Infrastructure.Repositories;

public class OrderRepository : IOrderRepository
{
    private readonly MongoDbContext _context;

    public OrderRepository(MongoDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Order>> GetAllAsync()
    {
        var dbOrders = await _context.Orders.Find(_ => true).ToListAsync();
        return dbOrders.Select(OrderDbMapper.ToDomain);
    }

    public async Task<Order?> GetByIdAsync(Guid id)
    {
        var dbOrder = await _context.Orders.Find(o => o.Id == id.ToString()).FirstOrDefaultAsync();
        return dbOrder is null ? null : OrderDbMapper.ToDomain(dbOrder);
    }

    public async Task PlaceOrderWithTransactionAsync(Order order, ItemListing itemToUpdate)
    {
        using var session = _context.StartSession();
        session.StartTransaction();

        try
        {
            var orderDb = OrderDbMapper.ToDb(order);
            var itemDb = ItemListingDbMapper.ToDb(itemToUpdate);

            await _context.Orders.InsertOneAsync(session, orderDb);

            var filter = Builders<ItemListingDbModel>.Filter.Eq(x => x.Id, itemDb.Id);
            await _context.ItemListings.ReplaceOneAsync(session, filter, itemDb);

            await session.CommitTransactionAsync();
        }
        catch
        {
            await session.AbortTransactionAsync();
            throw;
        }
    }
}