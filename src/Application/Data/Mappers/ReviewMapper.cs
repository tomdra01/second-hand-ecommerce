using Application.Data.DTOs;
using Domain.Entities;

namespace Application.Data.Mappers;

public static class ReviewMapper
{
    public static ReviewDto ToDto(Review entity) => new()
    {
        SellerId = entity.SellerId,
        ReviewerId = entity.ReviewerId,
        Rating = entity.Rating,
        Comment = entity.Comment,
        CreatedAt = entity.CreatedAt
    };

    public static Review ToEntity(ReviewDto dto) => new()
    {
        SellerId = dto.SellerId,
        ReviewerId = dto.ReviewerId,
        Rating = dto.Rating,
        Comment = dto.Comment,
        CreatedAt = dto.CreatedAt
    };
}