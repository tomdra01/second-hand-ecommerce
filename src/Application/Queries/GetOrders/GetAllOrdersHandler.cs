using Application.DTOs;
using Application.Interfaces;
using Application.Mappers;

namespace Application.Queries.GetOrders;

public class GetAllOrdersHandler
{
    private readonly IOrderRepository _orderRepository;
    private readonly ICacheService _cache;
    private const string CacheKey = "orders_all";

    public GetAllOrdersHandler(IOrderRepository orderRepository, ICacheService cache)
    {
        _orderRepository = orderRepository;
        _cache = cache;
    }

    public async Task<IEnumerable<OrderDto>> HandleAsync(GetAllOrdersQuery query)
    {
        var cached = await _cache.GetAsync<IEnumerable<OrderDto>>(CacheKey);
        if (cached is not null) return cached;

        var orders = await _orderRepository.GetAllAsync(); 
        var dtos = orders.Select(OrderMapper.ToDto).ToList();
        await _cache.SetAsync(CacheKey, dtos, TimeSpan.FromMinutes(5));
        return dtos;
    }
}