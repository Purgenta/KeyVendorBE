using AutoMapper;
using KeyVendor.Application.Common.Dto;
using KeyVendor.Application.Common.Dto.Category;
using KeyVendor.Application.Common.Dto.Key;
using KeyVendor.Application.Common.Dto.Vendor;

namespace KeyVendor.Application.Mappers.Key;

public class KeyProfile : Profile
{
    public KeyProfile()
    {
        CreateMap<Domain.Entities.Key, HiddenKeyDto>().ConstructUsing(x => HiddenDetails(x).Result);
        CreateMap<IEnumerable<Domain.Entities.Key>, PaginatedKey>().ConstructUsing(x => PaginatedHiddenKey(x).Result);
    }


    public async Task<HiddenKeyDto> HiddenDetails(Domain.Entities.Key key)
    {
        var category = await key.Category.ToEntityAsync();
        var vendor = await key.Vendor.ToEntityAsync();
        var user = await key.CreatedBy.ToEntityAsync();
        return new HiddenKeyDto(key.ID, key.Price, key.LicensedFor, key.Name,
            category.ID, vendor.ID, user.Email, key.Tax, key.ValidUntil.Date);
    }

    public async Task<PaginatedKey> PaginatedHiddenKey(IEnumerable<Domain.Entities.Key> keys)
    {
        var res = new List<HiddenKeyDto>();
        foreach (var key in keys)
        {
            res.Add(await this.HiddenDetails(key));
        }

        return new PaginatedKey(res, new PaginationDto(0, 0));
    }
}