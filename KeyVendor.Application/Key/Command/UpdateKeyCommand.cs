using KeyVendor.Application.Common.Dto.Key;
using KeyVendor.Application.Common.Interfaces;
using MediatR;
using MongoDB.Entities;

namespace KeyVendor.Application.Key;

public record UpdateKeyCommand(string Id, CreateKeyDto dto, Domain.Entities.User user) : IRequest;

public class UpdateKeyCommandHandler : IRequestHandler<UpdateKeyCommand>
{
    private readonly IFileUploadService _service;

    public UpdateKeyCommandHandler(IFileUploadService service)
    {
        this._service = service;
    }

    public async Task Handle(UpdateKeyCommand request, CancellationToken cancellationToken)
    {
        var key = await DB.Find<Domain.Entities.Key>().OneAsync(ID: request.Id, cancellation: cancellationToken);
        if (key == null) throw new Exception("Such a key doesn't exist");
        if (key.CreatedBy.Email != request.user.Email) throw new Exception("You're not allowed to make changes");
        var category = await key.Category.ToEntityAsync(cancellation: cancellationToken);
        var vendor = await key.Category.ToEntityAsync(cancellation: cancellationToken);
        if (request.dto.CategoryId != category!.ID)
        {
            var newCategory = await DB.Find<Domain.Entities.Category>()
                .OneAsync(request.dto.CategoryId, cancellation: cancellationToken);
            if (newCategory == null) throw new Exception("Invalid category");
            await category.Keys.RemoveAsync(key, cancellation: cancellationToken);
            await newCategory.Keys.AddAsync(key, cancellation: cancellationToken);
        }

        if (request.dto.VendorId != vendor!.ID)
        {
            var newVendor = await DB.Find<Domain.Entities.Vendor>()
                .OneAsync(request.dto.VendorId, cancellation: cancellationToken);
            if (newVendor == null) throw new Exception("Invalid vendor");
            await vendor.Keys.RemoveAsync(key, cancellation: cancellationToken);
            await newVendor.Keys.AddAsync(key, cancellation: cancellationToken);
        }
        await _service.SaveKeyImage(request.dto.Photo, key.ID);
    }
}