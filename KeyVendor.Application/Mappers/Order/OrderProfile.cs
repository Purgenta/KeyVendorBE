using AutoMapper;
using KeyVendor.Application.Common.Dto.Order;

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
        
    }

    public async Task<IEnumerable<PaginatedOrderDto>> PaginatedOrderDto(IEnumerable<Domain.Entities.Order> orders)
    {
    }
}