// Application.Services/OrderService.cs

using Application.Commands.CreateOrder;
using Application.Data.DTOs;
using Application.Interfaces;
using Application.Queries.GetOrderById;
using Application.Queries.GetOrders;

namespace Application.Services;

public class OrderService : IOrderService
{
    private readonly CreateOrderHandler _createHandler;
    private readonly GetAllOrdersHandler _getAllHandler;
    private readonly GetOrderByIdHandler _getByIdHandler;

    public OrderService(
        CreateOrderHandler createHandler,
        GetAllOrdersHandler getAllHandler,
        GetOrderByIdHandler getByIdHandler)
    {
        _createHandler = createHandler;
        _getAllHandler = getAllHandler;
        _getByIdHandler = getByIdHandler;
    }

    public async Task<string> CreateAsync(CreateOrderCommand command)
    {
        return await _createHandler.HandleAsync(command);
    }

    public async Task<IEnumerable<OrderDto>> GetAllAsync()
    {
        return await _getAllHandler.HandleAsync(new GetAllOrdersQuery());
    }

    public async Task<OrderDto?> GetByIdAsync(Guid id)
    {
        return await _getByIdHandler.HandleAsync(new GetOrderByIdQuery { Id = id });
    }
}