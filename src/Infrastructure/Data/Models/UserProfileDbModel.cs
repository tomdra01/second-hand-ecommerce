namespace Infrastructure.Data.Models;

public class UserProfileDbModel
{
    public string Id { get; set; } = default!;
    public string Username { get; set; } = default!;
    public string Email { get; set; } = default!;
    public string? Bio { get; set; }
}