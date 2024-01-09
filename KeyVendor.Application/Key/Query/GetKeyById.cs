using AutoMapper;
using KeyVendor.Application.Common.Dto.Key;
using KeyVendor.Application.Mappers.Key;
using MediatR;
using MongoDB.Entities;

namespace KeyVendor.Application.Key.Query;

public record GetKeyById(string Id) : IRequest<HiddenKeyDto>;

public class GetKeyByIdHandler : IRequestHandler<GetKeyById, HiddenKeyDto>
{
    private readonly IMapper _mapper;

    public GetKeyByIdHandler(IMapper _mapper)
    {
        this._mapper = _mapper;
    }

    public async Task<HiddenKeyDto> Handle(GetKeyById request, CancellationToken cancellationToken)
    {
        var key = await DB.Find<Domain.Entities.Key>().OneAsync(ID: request.Id, cancellation: cancellationToken);
        return _mapper.Map<HiddenKeyDto>(key);
    }
}