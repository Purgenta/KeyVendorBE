using Microsoft.AspNetCore.Http;

namespace KeyVendor.Application.Common.Dto.Key;

public record UpdateKeyDto(string Value, string Name, string ValidUntil, List<string>? LicensedFor, string CategoryId,
    string VendorId, IFormFile? Photo, double Price);