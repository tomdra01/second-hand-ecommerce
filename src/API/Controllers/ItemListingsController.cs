using Application.DTOs;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ItemListingsController : ControllerBase
{
    private readonly IItemListingService _service;
    private readonly ILogger<ItemListingsController> _logger;

    public ItemListingsController(IItemListingService service, ILogger<ItemListingsController> logger)
    {
        _service = service;
        _logger = logger;
    }

    /// <summary>
    /// Get all item listings.
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ItemListingDto>>> GetAll()
    {
        var listings = await _service.GetAllAsync();
        return Ok(listings);
    }

    /// <summary>
    /// Get a specific item listing by ID.
    /// </summary>
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<ItemListingDto>> GetById(Guid id)
    {
        var listing = await _service.GetByIdAsync(id);
        if (listing is null)
        {
            _logger.LogWarning("Listing with ID {Id} not found.", id);
            return NotFound(new { message = "Listing not found." });
        }

        return Ok(listing);
    }

    /// <summary>
    /// Create a new item listing (no images yet).
    /// </summary>
    [HttpPost]
    public async Task<ActionResult> Create([FromBody] ItemListingDto dto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        await _service.CreateAsync(dto);
        _logger.LogInformation("Created listing: {@Listing}", dto);
        return Ok(new { message = "Listing created successfully." });
    }
}
