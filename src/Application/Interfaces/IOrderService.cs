using Application.Commands.CreateOrder;
using Application.DTOs;

namespace Application.Interfaces;

public interface IOrderService
{
    Task<IEnumerable<OrderDto>> GetAllAsync();
    Task<OrderDto?> GetByIdAsync(Guid id);
    Task<string> CreateAsync(CreateOrderCommand command);
}