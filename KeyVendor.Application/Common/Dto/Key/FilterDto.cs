namespace KeyVendor.Application.Common.Dto.Key;

public record FilterDto(int Page, int Size, string? SearchQuery, string? CategoryId, string? Email);