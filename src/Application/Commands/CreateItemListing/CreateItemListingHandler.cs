using Application.Interfaces;
using Domain.Entities;

namespace Application.Commands.CreateItemListing;

public class CreateItemListingHandler
{
    private readonly IItemListingRepository _repository;
    private readonly ICacheService _cache;
    private readonly IFileStorageService _fileStorage;

    public CreateItemListingHandler(IItemListingRepository repo, ICacheService cache, IFileStorageService fileStorage)
    {
        _repository = repo;
        _cache = cache;
        _fileStorage = fileStorage;
    }

    public async Task<string> HandleAsync(CreateItemListingCommand command)
    {
        string imageUrl = string.Empty;

        if (command.Image != null)
        {
            var fileName = $"{Guid.NewGuid()}_{command.Image.FileName}";
            await _fileStorage.UploadFileAsync(
                fileName,
                command.Image.Content,
                command.Image.ContentType
            );
            imageUrl = _fileStorage.GetFileUrl(fileName);
        }

        var entity = new ItemListing
        {
            Title = command.Title,
            Description = command.Description,
            Price = command.Price,
            SellerId = command.SellerId,
            ImageUrls = string.IsNullOrEmpty(imageUrl) ? new() : new List<string> { imageUrl }
        };

        await _repository.CreateAsync(entity);
        await _cache.RemoveAsync("item_listings_all");

        return entity.Id.ToString();
    }
}