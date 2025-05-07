using Domain.Entities;

namespace Application.Interfaces;

public interface IOrderRepository
{
    Task<Order?> GetByIdAsync(Guid id);
    Task<IEnumerable<Order>> GetAllAsync(); 
    Task PlaceOrderWithTransactionAsync(Order order, ItemListing itemToUpdate);
}