using Domain.Entities;
using Infrastructure.Data.Models;

namespace Infrastructure.Data.Mappings;

public static class UserProfileDbMapper
{
    public static UserProfile ToDomain(UserProfileDbModel db) => new()
    {
        Id = Guid.Parse(db.Id),
        Username = db.Username,
        Email = db.Email,
        Bio = db.Bio
    };

    public static UserProfileDbModel ToDb(UserProfile entity) => new()
    {
        Id = entity.Id.ToString(),
        Username = entity.Username,
        Email = entity.Email,
        Bio = entity.Bio
    };
}