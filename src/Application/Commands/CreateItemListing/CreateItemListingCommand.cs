using Application.Data.DTOs;

namespace Application.Commands.CreateItemListing;

public class CreateItemListingCommand
{
    public string Title { get; init; } = string.Empty;
    public string Description { get; init; } = string.Empty;
    public decimal Price { get; init; }
    public string SellerId { get; init; } = string.Empty;
    public FileUploadDto? Image { get; init; }
}