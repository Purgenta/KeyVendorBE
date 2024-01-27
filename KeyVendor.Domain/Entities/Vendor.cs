using MongoDB.Entities;
using MongoDbGenericRepository.Attributes;

namespace KeyVendor.Domain.Entities;

[CollectionName("vendor")]
public class Vendor : BaseEntity
{
    [OwnerSide] public Many<Key> Keys { get; set; }
    public One<User> CreatedBy { get; set; }

    public Vendor()
    {
        this.InitOneToMany(() => Keys);
    }

    public string Name { get; set; }
}