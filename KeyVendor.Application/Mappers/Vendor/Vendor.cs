using KeyVendor.Application.Common.Dto.Vendor;

namespace KeyVendor.Application.Mappers.Vendor;

using AutoMapper;

public class Vendor : Profile
{
    public Vendor()
    {
        CreateMap<Domain.Entities.Vendor, VendorDto>().ConstructUsing(x => VendorDetails(x));
    }

    public static VendorDto VendorDetails(Domain.Entities.Vendor vendor)
    {
        return new VendorDto(vendor.Name, vendor.ID);
    }
}