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
        if (!string.IsNullOrEmpty(filters.SearchQuery))
        {
            query = (PagedSearch<Domain.Entities.Key>)query.Match(x => x.Name
                .ToUpper()
                .Contains(filters.SearchQuery.ToUpper()));
        }

        query = query.Match(x => x.Category.ID == filters.CategoryId).Match(x => x.Active == true);
        return query;
    }
}