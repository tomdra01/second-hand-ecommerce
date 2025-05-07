using Application.DTOs;

namespace Application.Interfaces;

public interface IOrderService
{
    Task PlaceOrderAsync(OrderDto orderDto);
}