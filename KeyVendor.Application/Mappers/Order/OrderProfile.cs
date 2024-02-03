using AutoMapper;
using KeyVendor.Application.Common.Dto;
using KeyVendor.Application.Common.Dto.Order;
using KeyVendor.Domain.Entities;
using MongoDB.Entities;

namespace KeyVendor.Application.Mappers.Order;

public class OrderProfile : Profile
{
    public OrderProfile()
    {
        CreateMap<IEnumerable<Domain.Entities.Order>, IEnumerable<PaginatedOrderDto>>()
            .ConstructUsing(x => PaginatedOrderDto(x).Result);
        CreateMap<Domain.Entities.Order, OrderDto>().ConstructUsing(x => OrderDto(x).Result);
    }

    public async Task<OrderDto> OrderDto(Domain.Entities.Order order)
    {
        var buyer = order.Buyer;
        var seller = order.Seller;
        var keys = await DB.Find<Domain.Entities.Key>().Match(key => order.Keys.Contains(key)).ExecuteAsync();
        var keyDto = new List<KeyOrderDto>();
        foreach (var key in keys)
        {
            string Value = order.Status == OrderStatus.Completed ? key.Value : null;
            keyDto.Add(new KeyOrderDto(Value, key.ID, key.Name));
        }

        return new OrderDto(order.ID, buyer.Email, seller.Email, order.Status, keyDto, order.CreatedOn,
            order.ModifiedOn,
            order.TotalPrice);
    }

    public async Task<IEnumerable<PaginatedOrderDto>> PaginatedOrderDto(IEnumerable<Domain.Entities.Order> orders)
    {
        var res = new List<OrderDto>();
        foreach (var order in orders)
        {
            res.Add(await this.OrderDto(order));
        }

        return new List<PaginatedOrderDto> { new(res, new PaginationDto(0, 0)) };
    }
}