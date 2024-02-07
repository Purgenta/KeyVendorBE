using KeyVendor.Application.Common.Dto.Order;
using KeyVendor.Domain.Entities;
using MongoDB.Entities;

namespace KeyVendor.Application.Common.Extensions.Order;

public static class OrderFilterExtension
{
    public static PagedSearch<Domain.Entities.Order, Domain.Entities.Order>
        ApplyFilters(this PagedSearch<Domain.Entities.Order,
                Domain.Entities.Order> query,
            FilterOrderDto filters)
    {
        if (filters.Buyer != null)
            query = query.Match(order => order.Buyer.UserName.Equals(filters.Buyer))
                ;

        if (filters.Seller != null) query = query.Match(order => order.Seller.Email.Equals(filters.Seller));
        if (filters.Status != null)
        {
            switch (filters.Status)
            {
                case OrderStatus.Completed:
                    query = query.Match(order => order.Status == Domain.Entities.OrderStatus.Completed);
                    break;
                case OrderStatus.Pending:
                    query = query.Match(order => order.Status == Domain.Entities.OrderStatus.Pending);
                    break;
                case OrderStatus.Cancelled:
                    query = query.Match(order => order.Status == Domain.Entities.OrderStatus.Cancelled);
                    break;
            }
        }

        return query;
    }
}