using AspNetCore.Identity.MongoDbCore.Models;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Entities;

namespace KeyVendor.Domain.Entities;

[Collection("user")]
public class User : MongoIdentityUser<ObjectId>, IEntity
{
    public double Money { get; set; }
    public Many<Order> Orders { get; set; }

    public Many<Key> Keys { get; set; }

    public Many<Vendor> CreatedVendors { get; set; }

    public string GenerateNewID()
    {
        return ObjectId.GenerateNewId().ToString();
    }

    public User()
    {
        this.InitOneToMany(() => CreatedVendors);
        this.InitOneToMany(() => Orders);
        this.InitOneToMany(() => Keys);
    }

    [BsonIgnore]
    public string? ID
    {
        get { return Id.ToString(); }
        set { Id = ObjectId.Parse(value); }
    }
}