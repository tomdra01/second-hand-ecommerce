using API.Requests;
using Application.Commands.CreateUserProfile;

namespace API.Mappers;

public static class UserProfileRequestMapper
{
    public static CreateUserProfileCommand ToCommand(CreateUserProfileRequest request) => new()
    {
        Username = request.Username,
        Email = request.Email,
        Bio = request.Bio
    };
}