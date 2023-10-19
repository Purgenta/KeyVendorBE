namespace KeyVendor.Application.Common.Dto.Key;

public record CreateKeyDto(string Value, DateTime ValidUntil, List<string> LicensedFor, string CategoryId);