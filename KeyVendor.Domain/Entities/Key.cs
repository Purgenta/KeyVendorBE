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
    public DateTime ValidUntil { get; set; }
    private double Price { get; set; }

    public Key(string value, User createdBy, List<string> licensedFor, DateTime validUntil, double price)
    {
        this.ValidUntil = validUntil;
        this.Value = value;
        this.CreatedBy = createdBy;
        this.LicensedFor = licensedFor;
        this.Price = price;
    }
}