using AspNetCore.Identity.MongoDbCore.Models;
using MongoDB.Bson;
using MongoDbGenericRepository.Attributes;

namespace KeyVendor.Domain.Entities;

[CollectionName("Roles")]
public class Role : MongoIdentityRole<ObjectId>
{
}