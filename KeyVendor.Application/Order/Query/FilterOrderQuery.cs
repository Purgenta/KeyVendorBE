using AutoMapper;
using KeyVendor.Application.Common.Dto;
using KeyVendor.Application.Common.Dto.Order;
using KeyVendor.Application.Common.Extensions.Order;
using MediatR;
using MongoDB.Entities;

namespace KeyVendor.Application.Order.Query;

public record FilterOrderQuery(FilterOrderDto Dto) : IRequest<PaginatedOrderDto>;

public class FilterOrderQueryHandler : IRequestHandler<FilterOrderQuery, PaginatedOrderDto>
{
    private readonly IMapper _mapper;

    public FilterOrderQueryHandler(IMapper mapper)
    {
        this._mapper = mapper;
    }

    public async Task<PaginatedOrderDto> Handle(FilterOrderQuery request, CancellationToken cancellationToken)
    {
        var order = await DB.PagedSearch<Domain.Entities.Order>().ApplyFilters(request.Dto)
            .Sort(x => x.CreatedOn, MongoDB.Entities.Order.Descending).ExecuteAsync(cancellationToken);
        var orderResponse = this._mapper.Map<PaginatedOrderDto>(order.Results)
            .AddPagination(new PaginationDto(order.TotalCount, order.PageCount));
        return orderResponse;
    }
}