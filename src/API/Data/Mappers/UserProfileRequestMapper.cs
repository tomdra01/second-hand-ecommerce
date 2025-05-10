using API.Data.Requests;
using Application.Commands.CreateUserProfile;

namespace API.Data.Mappers;

public static class UserProfileRequestMapper
{
    public static CreateUserProfileCommand ToCommand(CreateUserProfileRequest request) => new()
    {
        Username = request.Username,
        Email = request.Email,
        Bio = request.Bio
    };
}