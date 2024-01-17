using AspNetCore.Identity.MongoDbCore.Models;
using MongoDB.Bson;
using MongoDB.Entities;

namespace KeyVendor.Domain.Entities;

[Collection("user")]
public class User : MongoIdentityUser<Guid>, IEntity
{
    public double Money { get; set; }
    public Many<Order> Orders { get; set; }

    public string GenerateNewID()
    {
        return ObjectId.GenerateNewId().ToString();
    }

    public User()
    {
        this.InitOneToMany(() => Orders);
    }

    public string? ID { get; set; }
}