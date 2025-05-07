using API.Mappers;
using API.Requests;
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
    /// Place an order for an item.
    /// </summary>
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateOrderRequest request)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var dto = CreateOrderRequestMapper.ToDto(request);

        try
        {
            await _orderService.PlaceOrderAsync(dto);
            _logger.LogInformation("Order placed for item {ItemId} by buyer {BuyerId}", request.ItemId, request.BuyerId);

            return Ok(new { message = "Order placed successfully" });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to place order");
            return StatusCode(500, new { message = "Failed to place order", details = ex.Message });
        }
    }
}