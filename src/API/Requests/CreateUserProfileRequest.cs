namespace API.Requests;

public class CreateUserProfileRequest
{
    public string Username { get; set; } = default!;
    public string Email { get; set; } = default!;
    public string? Bio { get; set; }
}