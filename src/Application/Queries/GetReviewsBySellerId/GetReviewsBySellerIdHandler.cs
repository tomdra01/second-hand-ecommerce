using Application.DTOs;
using Application.Interfaces;
using Application.Mappers;

namespace Application.Queries.GetReviewsBySellerId;

public class GetReviewsBySellerIdHandler
{
    private readonly IReviewRepository _repository;
    private readonly ICacheService _cache;
    private const string CacheKeyPrefix = "reviews_seller_";

    public GetReviewsBySellerIdHandler(IReviewRepository repository, ICacheService cache)
    {
        _repository = repository;
        _cache = cache;
    }

    public async Task<IEnumerable<ReviewDto>> HandleAsync(GetReviewsBySellerIdQuery query)
    {
        var cacheKey = $"{CacheKeyPrefix}{query.SellerId}";
        var cached = await _cache.GetAsync<IEnumerable<ReviewDto>>(cacheKey);
        if (cached is not null) return cached;

        var reviews = await _repository.GetBySellerIdAsync(query.SellerId);
        var dtos = reviews.Select(ReviewMapper.ToDto).ToList();

        await _cache.SetAsync(cacheKey, dtos, TimeSpan.FromMinutes(5));
        return dtos;
    }
}