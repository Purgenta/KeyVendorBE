using KeyVendor.Application.Common.Dto.Order;
using MediatR;
using MongoDB.Entities;


namespace KeyVendor.Application.Order.Query;

public record OverviewQuery(OverviewOrderDto Request) : IRequest<OrderOverviewDetailsDto>;

public class OverviewQueryHandler : IRequestHandler<OverviewQuery, OrderOverviewDetailsDto>
{
    public async Task<OrderOverviewDetailsDto> Handle(OverviewQuery request, CancellationToken cancellationToken)
    {
        var orders = await DB.Find<Domain.Entities.Order>()
            .Match(x => x.CreatedOn >= request.Request.StartDate && x.CreatedOn <= request.Request.EndDate &&
                        x.Status == request.Request.Status)
            .ExecuteAsync(cancellationToken);
        var totalOrdersSold = orders.Count();
        var totalKeysSold = orders.SelectMany(x => x.Keys).Count();
        var totalPrice = orders.Sum(x => x.TotalPrice);
        return new OrderOverviewDetailsDto(totalOrdersSold, totalPrice, totalKeysSold);
    }
}