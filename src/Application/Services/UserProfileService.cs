using Application.Commands.CreateUserProfile;
using Application.Data.DTOs;
using Application.Interfaces;
using Application.Queries.GetUserProfiles;

namespace Application.Services;

public class UserProfileService : IUserProfileService
{
    private readonly CreateUserProfileHandler _createHandler;
    private readonly GetAllUserProfilesHandler _getAllHandler;

    public UserProfileService(
        CreateUserProfileHandler createHandler,
        GetAllUserProfilesHandler getAllHandler)
    {
        _createHandler = createHandler;
        _getAllHandler = getAllHandler;
    }

    public async Task CreateAsync(CreateUserProfileCommand command)
    {
        await _createHandler.HandleAsync(command);
    }

    public async Task<IEnumerable<UserProfileDto>> GetAllAsync()
    {
        return await _getAllHandler.HandleAsync(new GetAllUserProfilesQuery());
    }
}