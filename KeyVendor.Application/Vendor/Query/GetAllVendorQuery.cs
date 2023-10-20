using AutoMapper;
using KeyVendor.Application.Common.Dto.Vendor;
using MediatR;
using MongoDB.Entities;

namespace KeyVendor.Application.Vendor.Query;

public class GetAllVendorQuery : IRequest<List<VendorDto>>
{
}

public class GetAllVendorQueryHandler : IRequestHandler<GetAllVendorQuery, List<VendorDto>>
{
    private readonly IMapper _mapper;

    public GetAllVendorQueryHandler(IMapper mapper)
    {
        this._mapper = mapper;
    }

    public async Task<List<VendorDto>> Handle(GetAllVendorQuery request, CancellationToken cancellationToken)
    {
        var vendors = await DB.Find<Domain.Entities.Vendor>().Match(x => x.Name != "")
            .ExecuteAsync(cancellation: cancellationToken);
        return _mapper.Map<List<VendorDto>>(vendors);
    }
}