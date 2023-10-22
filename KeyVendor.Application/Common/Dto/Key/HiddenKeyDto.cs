using KeyVendor.Application.Common.Dto.Category;
using KeyVendor.Application.Common.Dto.Vendor;

namespace KeyVendor.Application.Common.Dto.Key;

public record HiddenKeyDto(string Id, double Price, List<string> LicensedFor, string Name, string CategoryId,
    string VendorId,
    string CreatedBy);


public record CategoryKeyDto(string Name, string Id);

public record VendorKeyDto(string Name, string Id);