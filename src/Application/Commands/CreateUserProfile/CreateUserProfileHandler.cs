using Application.Interfaces;
using Domain.Entities;

namespace Application.Commands.CreateUserProfile;

public class CreateUserProfileHandler
{
    private readonly IUserProfileRepository _repository;
    private readonly ICacheService _cache;
    private const string CacheKeyAll = "user_profiles_all";

    public CreateUserProfileHandler(IUserProfileRepository repository, ICacheService cache)
    {
        _repository = repository;
        _cache = cache;
    }

    public async Task<string> HandleAsync(CreateUserProfileCommand command)
    {
        var entity = new UserProfile
        {
            Id = Guid.NewGuid(),
            Username = command.Username,
            Email = command.Email,
            Bio = command.Bio
        };

        await _repository.CreateAsync(entity);
        await _cache.RemoveAsync(CacheKeyAll);

        return entity.Id.ToString();
    }
}