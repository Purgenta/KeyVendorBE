using KeyVendor.Application.Common.Dto.Order;
using KeyVendor.Application.Common.Extensions.Order;
using MediatR;
using MongoDB.Entities;

namespace KeyVendor.Application.Order.Query;

public record FilterOrderQuery(FilterOrderDto Dto) : IRequest;

public class FilterOrderQueryHandler : IRequestHandler<FilterOrderQuery>
{
    public async Task Handle(FilterOrderQuery request, CancellationToken cancellationToken)
    {
        var order = await DB.PagedSearch<Domain.Entities.Order>().ApplyFilters(request.Dto)
            .Sort(x => x.CreatedOn, MongoDB.Entities.Order.Descending).ExecuteAsync(cancellationToken);
    }
}