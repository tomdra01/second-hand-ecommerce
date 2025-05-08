using Application.DTOs;
using Application.Interfaces;
using Application.Mappers;

namespace Application.Queries.GetReviews;

public class GetAllReviewsHandler
{
    private readonly IReviewRepository _repository;
    private readonly ICacheService _cache;
    private const string CacheKey = "reviews_all";

    public GetAllReviewsHandler(IReviewRepository repository, ICacheService cache)
    {
        _repository = repository;
        _cache = cache;
    }

    public async Task<IEnumerable<ReviewDto>> HandleAsync(GetAllReviewsQuery query)
    {
        var cached = await _cache.GetAsync<IEnumerable<ReviewDto>>(CacheKey);
        if (cached is not null) return cached;

        var reviews = await _repository.GetAllAsync();
        var dtos = reviews.Select(ReviewMapper.ToDto).ToList();

        await _cache.SetAsync(CacheKey, dtos, TimeSpan.FromMinutes(5));
        return dtos;
    }
}