using Application.DTOs;
using Application.Interfaces;
using Application.Mappers;

namespace Application.Queries.GetUserProfiles;

public class GetAllUserProfilesHandler
{
    private readonly IUserProfileRepository _repository;
    private readonly ICacheService _cache;
    private const string CacheKey = "user_profiles_all";

    public GetAllUserProfilesHandler(IUserProfileRepository repository, ICacheService cache)
    {
        _repository = repository;
        _cache = cache;
    }

    public async Task<IEnumerable<UserProfileDto>> HandleAsync(GetAllUserProfilesQuery query)
    {
        var cached = await _cache.GetAsync<IEnumerable<UserProfileDto>>(CacheKey);
        if (cached is not null) return cached;

        var profiles = await _repository.GetAllAsync();
        var dtos = profiles.Select(UserProfileMapper.ToDto).ToList();
        await _cache.SetAsync(CacheKey, dtos, TimeSpan.FromMinutes(5));
        return dtos;
    }
}