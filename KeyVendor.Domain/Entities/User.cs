using AspNetCore.Identity.MongoDbCore.Models;
using MongoDB.Entities;

namespace KeyVendor.Domain.Entities;

[Collection("user")]
public class User : MongoIdentityUser<Guid>
{
    public double Money { get; set; }
}