using KeyVendor.Domain.Entities;
using MongoDB.Entities;

namespace KeyVendor.Application.Common.Dto.Order;

public record OverviewOrderDto(DateTime StartDate, DateTime EndDate);