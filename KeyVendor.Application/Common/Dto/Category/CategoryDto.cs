using KeyVendor.Domain.Entities;

namespace KeyVendor.Application.Common.Dto.Category;

public record CategoryDto(string Name, List<ChildCategory> ChildCategories, string Id);

public record ChildCategoriesDto(string Name, string Id);