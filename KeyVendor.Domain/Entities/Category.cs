using MongoDB.Entities;

namespace KeyVendor.Domain.Entities;

[Collection("category")]
public class Category : BaseEntity
{
    public string Name { get; set; }

    public Category()
    {
        this.Active = true;
        this.ChildCategories = new List<ChildCategory>();
    }

    public List<ChildCategory> ChildCategories { get; set; }
}

public record ChildCategory(string Name, string Id);