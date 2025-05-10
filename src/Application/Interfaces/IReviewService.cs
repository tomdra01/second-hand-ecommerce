using Application.Commands.CreateReview;
using Application.Data.DTOs;

namespace Application.Interfaces;

public interface IReviewService
{
    Task CreateAsync(CreateReviewCommand command);
    Task<IEnumerable<ReviewDto>> GetBySellerAsync(string sellerId);
    Task<IEnumerable<ReviewDto>> GetAllAsync();
}