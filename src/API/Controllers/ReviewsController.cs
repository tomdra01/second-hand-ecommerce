using API.Data.Mappers;
using API.Data.Requests;
using Application.Data.DTOs;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ReviewsController : ControllerBase
{
    private readonly IReviewService _service;

    public ReviewsController(IReviewService service)
    {
        _service = service;
    }

    /// <summary>
    /// Create a new review.
    /// </summary>
    [HttpGet("seller/{sellerId}")]
    public async Task<ActionResult<IEnumerable<ReviewDto>>> GetBySeller(string sellerId)
    {
        var reviews = await _service.GetBySellerAsync(sellerId);
        
        if (!reviews.Any())
            return NotFound(new { message = "No reviews found for this seller." });

        return Ok(reviews);
    }

    /// <summary>
    /// Get a specific review by ID.
    /// </summary>
    [HttpPost]
    public async Task<ActionResult> Create([FromBody] CreateReviewRequest request)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var command = CreateReviewRequestMapper.ToCommand(request);
        await _service.CreateAsync(command);

        return Ok(new { message = "Review submitted successfully" });
    }
    
    /// <summary>
    /// Get all reviews.
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ReviewDto>>> GetAll()
    {
        var reviews = await _service.GetAllAsync();
        return Ok(reviews);
    }
}