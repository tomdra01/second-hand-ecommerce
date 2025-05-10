namespace Application.Commands.CreateUserProfile;

public class CreateUserProfileCommand
{
    public string Username { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Bio { get; set; } = string.Empty;
}