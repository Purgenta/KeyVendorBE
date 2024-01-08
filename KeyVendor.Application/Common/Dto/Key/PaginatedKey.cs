namespace KeyVendor.Application.Common.Dto.Key;

public record PaginatedKey(List<HiddenKeyDto> Data, PaginationDto Pagination)
{
    internal PaginatedKey AddPagination(PaginationDto paginationDto)
    {
        return this with { Pagination = paginationDto };
    }
}