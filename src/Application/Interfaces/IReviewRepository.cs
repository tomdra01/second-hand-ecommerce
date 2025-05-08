using Domain.Entities;

namespace Application.Interfaces;

public interface IReviewRepository
{
    Task CreateAsync(Review review);
    Task<IEnumerable<Review>> GetBySellerIdAsync(string sellerId);
    Task<IEnumerable<Review>> GetAllAsync();
}