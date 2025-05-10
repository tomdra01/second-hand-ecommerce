using API.Data.Mappers;
using API.Data.Requests;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserProfilesController : ControllerBase
{
    private readonly IUserProfileService _service;

    public UserProfilesController(IUserProfileService service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateUserProfileRequest request)
    {
        var command = UserProfileRequestMapper.ToCommand(request);
        await _service.CreateAsync(command);
        return Ok(new { message = "User profile created." });
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var profiles = await _service.GetAllAsync();
        return Ok(profiles);
    }
}