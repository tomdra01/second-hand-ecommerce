using API.Mappers;
using API.Requests;
using Application.DTOs;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ItemListingsController : ControllerBase
{
    private readonly IItemListingService _itemListingService;
    private readonly ILogger<ItemListingsController> _logger;

    public ItemListingsController(IItemListingService itemListingService, ILogger<ItemListingsController> logger)
    {
        _itemListingService = itemListingService;
        _logger = logger;
    }

    /// <summary>
    /// Get all item listings.
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ItemListingDto>>> GetAll()
    {
        var listings = await _itemListingService.GetAllAsync();
        return Ok(listings);
    }

    /// <summary>
    /// Get a specific item listing by ID.
    /// </summary>
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<ItemListingDto>> GetById(Guid id)
    {
        var listing = await _itemListingService.GetByIdAsync(id);
        if (listing is null)
        {
            _logger.LogWarning("Listing with ID {Id} not found.", id);
            return NotFound(new { message = "Listing not found." });
        }

        return Ok(listing);
    }

    /// <summary>
    /// Create a new item listing with optional image upload.
    /// </summary>
    [HttpPost]
    [Consumes("multipart/form-data")]
    public async Task<ActionResult> Create([FromForm] CreateItemListingRequest request)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var command = CreateItemListingRequestMapper.ToCommand(request);
        var createdId = await _itemListingService.CreateWithImageAsync(command);

        return Ok(new
        {
            message = "Listing created successfully",
            listingId = createdId
        });
    }
}
