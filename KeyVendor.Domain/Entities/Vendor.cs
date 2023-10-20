using MongoDbGenericRepository.Attributes;

namespace KeyVendor.Domain.Entities;

[CollectionName("vendor")]
public class Vendor : BaseEntity
{
    public Vendor(string name, User createdBy)
    {
        this.Name = name;
        this.CreatedBy = createdBy;
        this.Active = true;
    }

    public string Name { get; set; }
    public User CreatedBy { get; set; }
}