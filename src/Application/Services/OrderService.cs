using Application.DTOs;
using Application.Interfaces;
using Application.Mappers;

namespace Application.Services;

public class OrderService : IOrderService
{
    private readonly IOrderRepository _orderRepository;
    private readonly IItemListingRepository _itemListingRepository;

    public OrderService(IOrderRepository orderRepository, IItemListingRepository itemListingRepository)
    {
        _orderRepository = orderRepository;
        _itemListingRepository = itemListingRepository;
    }

    public async Task PlaceOrderAsync(OrderDto orderDto)
    {
        var item = await _itemListingRepository.GetByIdAsync(Guid.Parse(orderDto.ItemId));
        if (item == null)
            throw new Exception("Item not found");

        item.IsSold = true;

        var order = OrderMapper.ToEntity(orderDto);
        order.TotalPrice = item.Price * order.Quantity;

        await _orderRepository.PlaceOrderWithTransactionAsync(order, item);
    }
}