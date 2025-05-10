using Application.Data.DTOs;
using Domain.Entities;

namespace Application.Data.Mappers;

public static class UserProfileMapper
{
    public static UserProfileDto ToDto(UserProfile entity) => new()
    {
        Id = entity.Id.ToString(),
        Username = entity.Username,
        Email = entity.Email,
        Bio = entity.Bio
    };

    public static UserProfile ToEntity(UserProfileDto dto) => new()
    {
        Id = Guid.Parse(dto.Id),
        Username = dto.Username,
        Email = dto.Email,
        Bio = dto.Bio
    };
}