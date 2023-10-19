using AutoMapper;
using KeyVendor.Application.Common.Dto.Category;

namespace KeyVendor.Application.Mappers.Category;

public class Category : Profile
{
    public Category()
    {
        CreateMap<Domain.Entities.Category, CategoryDto>().ConstructUsing(x => CategoryDetails(x));
        CreateMap<List<Domain.Entities.Category>, IEnumerable<CategoryDto>>()
            .ConstructUsing(x => CategoryDetailsList(x));
    }

    public static CategoryDto CategoryDetails(Domain.Entities.Category category)
    {
        return new CategoryDto(category.Name, category.ChildCategories, category.ID);
    }

    public static IEnumerable<CategoryDto> CategoryDetailsList(List<Domain.Entities.Category> category)
    {
        var res = category.Select(cat => CategoryDetails(cat)).ToList();
        return res;
    }
}