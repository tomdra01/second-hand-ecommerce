using Application.Commands.CreateUserProfile;
using Application.DTOs;

namespace Application.Interfaces;

public interface IUserProfileService
{
    Task CreateAsync(CreateUserProfileCommand command);
    Task<IEnumerable<UserProfileDto>> GetAllAsync();
}