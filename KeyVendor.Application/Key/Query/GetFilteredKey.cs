using AutoMapper;
using KeyVendor.Application.Common.Dto;
using KeyVendor.Application.Common.Dto.Key;
using KeyVendor.Application.Common.Extensions.Key;
using MediatR;
using MongoDB.Entities;

namespace KeyVendor.Application.Key.Query;

public record GetFilteredKey(FilterDto FilterDto) : IRequest<PaginatedKey>;

public record GetFilteredKeyHandler : IRequestHandler<GetFilteredKey, PaginatedKey>
{
    private readonly IMapper _mapper;

    public GetFilteredKeyHandler(IMapper mapper)
    {
        this._mapper = mapper;
    }

    public async Task<PaginatedKey> Handle(GetFilteredKey request, CancellationToken cancellationToken)
    {
        var keys = await DB.PagedSearch<Domain.Entities.Key>().Sort(x => x.Name, MongoDB.Entities.Order.Ascending)
            .ApplyFilters(request.FilterDto)
            .PageNumber(request.FilterDto.Page).PageSize(request.FilterDto.Size)
            .ExecuteAsync(cancellation: cancellationToken);
        var paginated = this._mapper.Map<PaginatedKey>(keys.Results)
            .AddPagination(new PaginationDto(keys.TotalCount, keys.PageCount));
        return paginated;
    }
}