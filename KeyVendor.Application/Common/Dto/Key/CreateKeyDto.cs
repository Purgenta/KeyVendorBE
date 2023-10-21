using Microsoft.AspNetCore.Http;

namespace KeyVendor.Application.Common.Dto.Key;

public record CreateKeyDto(string Value, string ValidUntil, List<string>? LicensedFor, string CategoryId,
    string VendorId, IFormFile Photo, double Price);