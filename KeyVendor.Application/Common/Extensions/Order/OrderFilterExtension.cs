using KeyVendor.Application.Common.Dto.Order;
using MongoDB.Entities;

namespace KeyVendor.Application.Common.Extensions.Order;

public static class OrderFilterExtension
{
    public static PagedSearch<Domain.Entities.Order, Domain.Entities.Order>
        ApplyFilters(this PagedSearch<Domain.Entities.Order,
                Domain.Entities.Order> query,
            FilterOrderDto filters)
    {
        if (filters.Buyer != null) query = query.Match(order => order.Buyer.Email.Equals(filters.Buyer));
        if (filters.Seller != null) query = query.Match(order => order.Seller.Email.Equals(filters.Seller));
        if (filters.Status != null)
            query = query.Match(order => order.Status.Equals(order.Status.Equals(filters.Status)));
        return query;
    }
}