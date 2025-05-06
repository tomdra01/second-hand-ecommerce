using API.Requests;
using Application.DTOs;
using Application.Interfaces;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ItemListingsController : ControllerBase
{
    private readonly ICachedItemListingService _cachedService;
    private readonly MinioStorageService _minioService;
    private readonly ILogger<ItemListingsController> _logger;

    public ItemListingsController(
        ICachedItemListingService cachedService,
        MinioStorageService minioService,
        ILogger<ItemListingsController> logger)
    {
        _cachedService = cachedService;
        _minioService = minioService;
        _logger = logger;
    }

    /// <summary>
    /// Get all item listings.
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ItemListingDto>>> GetAll()
    {
        var listings = await _cachedService.GetAllAsync();
        return Ok(listings);
    }

    /// <summary>
    /// Get a specific item listing by ID.
    /// </summary>
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

    /// <summary>
    /// Create a new item listing with optional image upload.
    /// </summary>
    [HttpPost]
    [Consumes("multipart/form-data")]
    public async Task<ActionResult> Create([FromForm] CreateItemListingRequest request)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        string imageUrl = string.Empty;

        if (request.Image is not null)
        {
            var objectName = $"{Guid.NewGuid()}_{request.Image.FileName}";
            await _minioService.UploadFileAsync(request.Image, objectName);
            imageUrl = _minioService.GetFileUrl(objectName);
        }

        var dto = new ItemListingDto
        {
            Title = request.Title,
            Description = request.Description,
            Price = request.Price,
            SellerId = request.SellerId,
            ImageUrls = imageUrl != string.Empty ? new List<string> { imageUrl } : new()
        };

        await _cachedService.CreateAsync(dto);
        _logger.LogInformation("Created listing: {@Listing}", dto);

        return Ok(new
        {
            message = "Listing created successfully",
            listing = dto
        });
    }
}
