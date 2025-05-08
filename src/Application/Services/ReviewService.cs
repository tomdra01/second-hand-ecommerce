using Application.Commands.CreateReview;
using Application.DTOs;
using Application.Interfaces;
using Application.Queries.GetReviews;
using Application.Queries.GetReviewsBySellerId;

namespace Application.Services;

public class ReviewService : IReviewService
{
    private readonly CreateReviewHandler _createHandler;
    private readonly GetReviewsBySellerIdHandler _getHandler;
    private readonly GetAllReviewsHandler _getAllHandler;

    public ReviewService(CreateReviewHandler createHandler, GetReviewsBySellerIdHandler getHandler, GetAllReviewsHandler getAllHandler)
    {
        _getAllHandler = getAllHandler;
        _createHandler = createHandler;
        _getHandler = getHandler;
    }

    public async Task CreateAsync(CreateReviewCommand command)
    {
        await _createHandler.HandleAsync(command);
    }

    public async Task<IEnumerable<ReviewDto>> GetBySellerAsync(string sellerId)
    {
        return await _getHandler.HandleAsync(new GetReviewsBySellerIdQuery { SellerId = sellerId });
    }
    
    public async Task<IEnumerable<ReviewDto>> GetAllAsync()
    {
        return await _getAllHandler.HandleAsync(new GetAllReviewsQuery());
    }
}