using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Data;
using Infrastructure.Data.Mappings;
using MongoDB.Driver;

namespace Infrastructure.Repositories;

public class ReviewRepository : IReviewRepository
{
    private readonly MongoDbContext _context;

    public ReviewRepository(MongoDbContext context)
    {
        _context = context;
    }

    public async Task CreateAsync(Review review)
    {
        var dbReview = ReviewDbMapper.ToDb(review);
        await _context.Reviews.InsertOneAsync(dbReview);
    }

    public async Task<IEnumerable<Review>> GetBySellerIdAsync(string sellerId)
    {
        var dbReviews = await _context.Reviews.Find(r => r.SellerId == sellerId).ToListAsync();
        return dbReviews.Select(ReviewDbMapper.ToDomain);
    }
    
    public async Task<IEnumerable<Review>> GetAllAsync()
    {
        var dbReviews = await _context.Reviews.Find(_ => true).ToListAsync();
        return dbReviews.Select(ReviewDbMapper.ToDomain);
    }
}