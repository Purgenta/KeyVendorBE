using KeyVendor.Domain.Entities;
using MongoDB.Entities;

namespace KeyVendor.Application.Common.Dto.Order;

public record OrderDto(
    string Id,
    string BuyerEmail,
    string SellerEmail,
    OrderStatus OrderStatus,
    List<KeyOrderDto> Keys,
    DateTime CreatedAt,
    DateTime UpdatedAt,
    double TotalCost)
{
}

public record KeyOrderDto(string? Value, string ID, string Name);