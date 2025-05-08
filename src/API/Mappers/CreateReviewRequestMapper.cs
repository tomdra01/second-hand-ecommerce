using API.Requests;
using Application.Commands.CreateReview;

namespace API.Mappers;

public static class CreateReviewRequestMapper
{
    public static CreateReviewCommand ToCommand(CreateReviewRequest request) => new()
    {
        SellerId = request.SellerId,
        ReviewerId = request.ReviewerId,
        Rating = request.Rating,
        Comment = request.Comment
    };
}