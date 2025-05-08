using Application.Interfaces;
using Domain.Entities;

namespace Application.Commands.CreateOrder;

public class CreateOrderHandler
{
    private readonly IOrderRepository _orderRepository;
    private readonly IItemListingRepository _itemListingRepository;
    private readonly ICacheService _cache;

    public CreateOrderHandler(IOrderRepository orderRepository, IItemListingRepository itemListingRepository, ICacheService cache)
    {
        _orderRepository = orderRepository;
        _itemListingRepository = itemListingRepository;
        _cache = cache;
    }

    public async Task<string> HandleAsync(CreateOrderCommand command)
    {
        var item = await _itemListingRepository.GetByIdAsync(Guid.Parse(command.ItemId));
        if (item == null) throw new Exception("Item not found");

        item.IsSold = true;

        var order = new Order
        {
            Id = Guid.NewGuid(),
            ItemId = item.Id.ToString(),
            BuyerId = command.BuyerId,
            TotalPrice = item.Price,
            PlacedAt = DateTime.UtcNow
        };

        await _orderRepository.PlaceOrderWithTransactionAsync(order, item);
        
        await _cache.RemoveAsync("orders_all");
        await _cache.RemoveAsync("item_listings_all"); 

        return order.Id.ToString();
    }
}