using KeyVendor.Domain.Entities;

namespace KeyVendor.Application.Common.Dto.Order;

public record FilterByUser(OrderStatus Status, int Page, int Size);