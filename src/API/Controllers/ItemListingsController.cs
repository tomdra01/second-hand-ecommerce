using Application.DTOs;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ItemListingsController : ControllerBase
{
    private readonly ICachedItemListingService _cachedService;
    private readonly ILogger<ItemListingsController> _logger;

    public ItemListingsController(ICachedItemListingService cachedService, ILogger<ItemListingsController> logger)
    {
        _cachedService = cachedService;
        _logger = logger;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ItemListingDto>>> GetAll()
    {
        var listings = await _cachedService.GetAllAsync();
        return Ok(listings);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<ItemListingDto>> GetById(Guid id)
    {
        var listing = await _cachedService.GetByIdAsync(id);
        if (listing is null)
        {
            _logger.LogWarning("Listing with ID {Id} not found.", id);
            return NotFound(new { message = "Listing not found." });
        }

        return Ok(listing);
    }

    [HttpPost]
    public async Task<ActionResult> Create([FromBody] ItemListingDto dto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        await _cachedService.CreateAsync(dto);
        _logger.LogInformation("Created listing: {@Listing}", dto);
        return Ok(new { message = "Listing created successfully." });
    }
}