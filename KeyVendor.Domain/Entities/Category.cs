using MongoDB.Entities;

namespace KeyVendor.Domain.Entities;

[Collection("category")]
public class Category : BaseEntity
{
    public string Name { get; set; }
    public bool IsRoot { get; set; }
    [OwnerSide] public Many<Key> Keys { get; set; }

    public Category()
    {
        this.InitOneToMany(() => Keys);
        this.Active = true;
        this.ChildCategories = new List<ChildCategory>();
        this.IsRoot = false;
    }

    public List<ChildCategory> ChildCategories { get; set; }
}

public record ChildCategory(string Name, string Id);