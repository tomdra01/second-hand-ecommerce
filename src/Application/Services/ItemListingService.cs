using Application.Commands.CreateItemListing;
using Application.DTOs;
using Application.Interfaces;
using Application.Queries.GetItemListingById;
using Application.Queries.GetItemListings;

namespace Application.Services;

public class ItemListingService : IItemListingService
{
    private readonly GetAllItemListingHandler _getAllHandler;
    private readonly GetItemListingByIdHandler _getByIdHandler;
    private readonly CreateItemListingHandler _createHandler;

    public ItemListingService(
        GetAllItemListingHandler getAllHandler,
        GetItemListingByIdHandler getByIdHandler,
        CreateItemListingHandler createHandler)
    {
        _getAllHandler = getAllHandler;
        _getByIdHandler = getByIdHandler;
        _createHandler = createHandler;
    }

    public async Task<IEnumerable<ItemListingDto>> GetAllAsync()
    {
        return await _getAllHandler.HandleAsync(new GetAllItemListingQuery());
    }

    public async Task<ItemListingDto?> GetByIdAsync(Guid id)
    {
        return await _getByIdHandler.HandleAsync(new GetItemListingByIdQuery { Id = id });
    }

    public async Task<string> CreateWithImageAsync(CreateItemListingCommand command)
    {
        return await _createHandler.HandleAsync(command);
    }
}