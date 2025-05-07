using API.Mappers;
using API.Requests;
using Application.DTOs;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrdersController : ControllerBase
{
    private readonly IOrderService _orderService;
    private readonly ILogger<OrdersController> _logger;

    public OrdersController(IOrderService orderService, ILogger<OrdersController> logger)
    {
        _orderService = orderService;
        _logger = logger;
    }

    /// <summary>
    /// Get all orders.
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<OrderDto>>> GetAll()
    {
        var orders = await _orderService.GetAllAsync();
        return Ok(orders);
    }

    /// <summary>
    /// Get a specific order by ID.
    /// </summary>
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<OrderDto>> GetById(Guid id)
    {
        var order = await _orderService.GetByIdAsync(id);
        if (order is null)
        {
            _logger.LogWarning("Order with ID {Id} not found.", id);
            return NotFound(new { message = "Order not found." });
        }

        return Ok(order);
    }

    /// <summary>
    /// Place a new order.
    /// </summary>
    [HttpPost]
    public async Task<ActionResult> Create([FromBody] CreateOrderRequest request)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            var command = CreateOrderRequestMapper.ToCommand(request);
            var orderId = await _orderService.CreateAsync(command);

            _logger.LogInformation("Order placed for item {ItemId} by buyer {BuyerId}", request.ItemId, request.BuyerId);
            return Ok(new { message = "Order placed successfully", orderId });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to place order");
            return StatusCode(500, new { error = "Failed to place order", details = ex.Message });
        }
    }
}