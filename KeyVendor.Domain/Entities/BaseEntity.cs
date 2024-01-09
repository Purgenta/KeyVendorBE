using MongoDB.Entities;

namespace KeyVendor.Domain.Entities;

public class BaseEntity : Entity, ICreatedOn, IModifiedOn
{
    public bool Active { get; set; }
    public DateTime CreatedOn { get; set; }
    public DateTime ModifiedOn { get; set; }
}