using Domain.Entities;

namespace Application.Interfaces;

public interface IUserProfileRepository
{
    Task CreateAsync(UserProfile profile);
    Task<IEnumerable<UserProfile>> GetAllAsync();
}