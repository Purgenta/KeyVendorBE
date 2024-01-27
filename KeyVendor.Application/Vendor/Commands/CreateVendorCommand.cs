using KeyVendor.Application.Common.Contants;
using KeyVendor.Application.Common.Dto.Vendor;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using MongoDB.Entities;

namespace KeyVendor.Application.Vendor;

public record CreateVendorCommand(CreateVendorDto Req, Domain.Entities.User User) : IRequest;

public class CreateVendorCommandHandler : IRequestHandler<CreateVendorCommand>

{
    public async Task Handle(CreateVendorCommand request, CancellationToken cancellationToken)
    {
        var vendor = await DB.Find<Domain.Entities.Vendor>().Match(x => x.Name.Equals(request.Req.Name))
            .ExecuteSingleAsync(cancellationToken);
        if (vendor != null) throw new Exception("Such a vendor already exists");
        var createdVendor = new Domain.Entities.Vendor();
        createdVendor.CreatedBy = request.User.ToReference();
        createdVendor.Name = request.Req.Name;
        await createdVendor.SaveAsync(cancellation: cancellationToken);
        await request.User.CreatedVendors.AddAsync(createdVendor);
    }
}