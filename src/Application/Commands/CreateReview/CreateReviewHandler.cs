using Application.Interfaces;
using Domain.Entities;

namespace Application.Commands.CreateReview;

public class CreateReviewHandler
{
    private readonly IReviewRepository _repository;
    private readonly ICacheService _cache;
    private const string CacheKeyAll = "reviews_all";
    private const string CacheKeyPrefix = "reviews_seller_";

    public CreateReviewHandler(IReviewRepository repository, ICacheService cache)
    {
        _repository = repository;
        _cache = cache;
    }

    public async Task HandleAsync(CreateReviewCommand command)
    {
        var review = new Review
        {
            Id = Guid.NewGuid(),
            SellerId = command.SellerId,
            ReviewerId = command.ReviewerId,
            Rating = command.Rating,
            Comment = command.Comment,
            CreatedAt = DateTime.UtcNow
        };

        await _repository.CreateAsync(review);

        // Invalidate caches
        await _cache.RemoveAsync(CacheKeyAll);
        await _cache.RemoveAsync($"{CacheKeyPrefix}{command.SellerId}");
    }
}