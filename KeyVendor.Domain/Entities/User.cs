using AspNetCore.Identity.MongoDbCore.Models;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Entities;
using ThirdParty.Json.LitJson;

namespace KeyVendor.Domain.Entities;

[Collection("user")]
public class User : MongoIdentityUser<ObjectId>, IEntity
{
    public double Money { get; set; }


    public string GenerateNewID()
    {
        return ObjectId.GenerateNewId().ToString();
    }

    [BsonId]
    [BsonIgnore]
    public string? ID
    {
        get { return Id.ToString(); }
        set { Id = ObjectId.Parse(value); }
    }
}