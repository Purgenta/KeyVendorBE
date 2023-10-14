using AspNetCore.Identity.MongoDbCore.Models;
using MongoDbGenericRepository.Attributes;

namespace KeyVendor.Domain.Entities;

[CollectionName("Roles")]
public class Role : MongoIdentityRole<Guid>
{
}