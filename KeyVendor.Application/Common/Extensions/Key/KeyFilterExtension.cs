using KeyVendor.Application.Common.Dto.Key;
using MongoDB.Entities;

namespace KeyVendor.Application.Common.Extensions.Key;

public static class KeyFilterExtension
{
    public static PagedSearch<Domain.Entities.Key, Domain.Entities.Key>
        ApplyFilters(this PagedSearch<Domain.Entities.Key,
                Domain.Entities.Key> query,
            FilterDto filters)
    {
        query = query.Match(x => x.Active == true);
        if (!string.IsNullOrEmpty(filters.SearchQuery))
        {
            query = (PagedSearch<Domain.Entities.Key>)query.Match(x => x.Name
                .ToUpper()
                .Contains(filters.SearchQuery.ToUpper()));
        }

        if (filters.Price.HasValue)
        {
            query = query.Match(x => x.Price >= filters.Price);
        }

        if (!string.IsNullOrEmpty(filters.VendorId))
            query = query.Match(x => x.Vendor.ID == filters.VendorId);
        if (!string.IsNullOrEmpty(filters.CategoryId))
            query = query.Match(x => x.Category.ID == filters.CategoryId).Match(x => x.Active == true);
        if (!string.IsNullOrEmpty(filters.Email))
            query = query.Match(x => x.CreatedBy.Email!.ToString() == filters.Email);
        return query;
    }
}