using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Data;
using Infrastructure.Data.Mappings;
using MongoDB.Driver;

namespace Infrastructure.Repositories;

public class UserProfileRepository : IUserProfileRepository
{
    private readonly MongoDbContext _context;

    public UserProfileRepository(MongoDbContext context)
    {
        _context = context;
    }

    public async Task CreateAsync(UserProfile profile)
    {
        var dbModel = UserProfileDbMapper.ToDb(profile);
        await _context.UserProfiles.InsertOneAsync(dbModel);
    }

    public async Task<IEnumerable<UserProfile>> GetAllAsync()
    {
        var dbModels = await _context.UserProfiles.Find(_ => true).ToListAsync();
        return dbModels.Select(UserProfileDbMapper.ToDomain);
    }
}