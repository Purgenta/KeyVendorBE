namespace KeyVendor.Application.Common.Dto.Order;

public record PaginatedOrderDto(IEnumerable<OrderDto> Data, PaginationDto Pagination)
{
    internal PaginatedOrderDto AddPagination(PaginationDto paginationDto)
    {
        return this with { Pagination = paginationDto };
    }
}