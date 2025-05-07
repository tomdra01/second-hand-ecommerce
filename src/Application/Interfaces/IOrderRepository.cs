using Domain.Entities;

namespace Application.Interfaces;

public interface IOrderRepository
{
    Task PlaceOrderWithTransactionAsync(Order order, ItemListing itemToUpdate);
}