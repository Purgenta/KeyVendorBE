using KeyVendor.Domain.Entities;

namespace KeyVendor.Application.Common.Dto.Order;

public record OrderOverviewDetailsDto(int TotalOrdersSold, double TotalPrice, double TotalKeysSold);