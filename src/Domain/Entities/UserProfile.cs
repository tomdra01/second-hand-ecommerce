namespace Domain.Entities;

public class UserProfile
{
    public Guid Id { get; set; }
    public string Username { get; set; } = default!;
    public string Email { get; set; } = default!;
    public string? Bio { get; set; }
}