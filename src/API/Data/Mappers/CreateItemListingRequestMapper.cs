using API.Data.Requests;
using Application.Commands.CreateItemListing;
using Application.Data.DTOs;

namespace API.Data.Mappers;

public static class CreateItemListingRequestMapper
{
    public static CreateItemListingCommand ToCommand(CreateItemListingRequest request)
    {
        return new CreateItemListingCommand
        {
            Title = request.Title,
            Description = request.Description,
            Price = request.Price,
            SellerId = request.SellerId,
            Image = request.Image is not null
                ? new FileUploadDto
                {
                    FileName = request.Image.FileName,
                    ContentType = request.Image.ContentType,
                    Content = request.Image.OpenReadStream()
                }
                : null
        };
    }
}