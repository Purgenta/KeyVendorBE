using Amazon.Runtime.Internal.Endpoints.StandardLibrary;
using MongoDB.Entities;

namespace KeyVendor.Domain.Entities;

public class Key : BaseEntity
{
    public string Value { get; set; }
    public User CreatedBy { get; set; }
    public One<Category> Category { get; set; }
    public List<string> LicensedFor { get; set; }

    public One<Vendor> Vendor { get; set; }
    public Date ValidUntil { get; set; }
    public string ImageUrl { get; set; }

    public Key(string value, User createdBy, One<Category> category, List<string> licensedFor, Date validUntil,
        string imageUrl)
    {
        this.ValidUntil = validUntil;
        this.Value = value;
        this.CreatedBy = createdBy;
        this.Category = category;
        this.LicensedFor = licensedFor;
        this.ImageUrl = imageUrl;
    }
}