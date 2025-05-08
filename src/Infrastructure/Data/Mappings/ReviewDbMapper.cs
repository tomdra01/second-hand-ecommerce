using Domain.Entities;
using Infrastructure.Data.Models;

namespace Infrastructure.Data.Mappings;

public static class ReviewDbMapper
{
    public static ReviewDbModel ToDb(Review review) => new()
    {
        Id = review.Id.ToString(),
        SellerId = review.SellerId,
        ReviewerId = review.ReviewerId,
        Rating = review.Rating,
        Comment = review.Comment,
        CreatedAt = review.CreatedAt
    };

    public static Review ToDomain(ReviewDbModel dbModel) => new()
    {
        Id = Guid.Parse(dbModel.Id),
        SellerId = dbModel.SellerId,
        ReviewerId = dbModel.ReviewerId,
        Rating = dbModel.Rating,
        Comment = dbModel.Comment,
        CreatedAt = dbModel.CreatedAt
    };
}