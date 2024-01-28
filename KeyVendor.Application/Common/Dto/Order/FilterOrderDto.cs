using KeyVendor.Domain.Entities;

namespace KeyVendor.Application.Common.Dto.Order;

public record FilterOrderDto(string? Buyer, string? Seller, OrderStatus? Status, int Page, int Size);