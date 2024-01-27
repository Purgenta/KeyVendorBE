using KeyVendor.Application.Common.Dto.Key;
using KeyVendor.Application.Common.Interfaces;
using MediatR;
using MongoDB.Entities;

namespace KeyVendor.Application.Key;

public record CreateKeyCommand(CreateKeyDto Data, Domain.Entities.User user) : IRequest;

public class CreateKeyCommandHandler : IRequestHandler<CreateKeyCommand>
{
    private readonly IFileUploadService _fileUploadService;

    public CreateKeyCommandHandler(IFileUploadService fileUploadService)
    {
        this._fileUploadService = fileUploadService;
    }

    public async Task Handle(CreateKeyCommand request, CancellationToken cancellationToken)
    {
        var data = request.Data;
        var category = await DB.Find<Domain.Entities.Category>()
            .OneAsync(request.Data.CategoryId, cancellation: cancellationToken);
        if (category == null || category.ChildCategories.Count != 0)
            throw new Exception("No such category exists/Not a valid category");
        var vendor = await DB.Find<Domain.Entities.Vendor>()
            .OneAsync(request.Data.VendorId, cancellation: cancellationToken);
        if (vendor == null) throw new Exception("No such vendor exists");
        var key = new Domain.Entities.Key(data.Value, data.Name, request.user.ToReference(), data.LicensedFor,
            DateTime.Parse(data.ValidUntil),
            data.Price, data.Tax);
        key.Category = category.ToReference();
        key.Vendor = vendor.ToReference();
        key.Active = true;
        await key.SaveAsync(cancellation: cancellationToken);
        await category.Keys.AddAsync(key, cancellation: cancellationToken);
        await vendor.Keys.AddAsync(key, cancellation: cancellationToken);
        await _fileUploadService.SaveKeyImage(request.Data.Photo, key.ID);
    }
}