using KeyVendor.Application.Common.Dto.Order;
using KeyVendor.Domain.Entities;
using MediatR;

namespace KeyVendor.Application.Order.Query;

public record OrderOptionsQuery() : IRequest<List<OrderStatusDto>>;

public class OrderOptionsQueryHandler : IRequestHandler<OrderOptionsQuery, List<OrderStatusDto>>
{
    public Task<List<OrderStatusDto>> Handle(OrderOptionsQuery request, CancellationToken cancellationToken)
    {
        var dict = Enum.GetValues(typeof(OrderStatus))
            .Cast<OrderStatus>()
            .ToDictionary(t => (int)t, t => t.ToString());
        var list = dict.Select(t => new OrderStatusDto(t.Value, t.Key)).ToList();
        return Task.FromResult(list);
    }
}