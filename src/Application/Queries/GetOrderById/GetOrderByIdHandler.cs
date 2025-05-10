using Application.Data.DTOs;
using Application.Data.Mappers;
using Application.Interfaces;

namespace Application.Queries.GetOrderById;

public class GetOrderByIdHandler
{
    private readonly IOrderRepository _orderRepository;

    public GetOrderByIdHandler(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public async Task<OrderDto?> HandleAsync(GetOrderByIdQuery query)
    {
        var order = await _orderRepository.GetByIdAsync(query.Id);
        return order is null ? null : OrderMapper.ToDto(order);
    }
}