using Amazon.Runtime.Internal.Endpoints.StandardLibrary;
using MongoDB.Entities;

namespace KeyVendor.Domain.Entities;

public class Key : BaseEntity
{
    public string Value { get; set; }
    public string Name { get; set; }
    public User CreatedBy { get; set; }
    public One<Category> Category { get; set; }
    public List<string> LicensedFor { get; set; }

    public One<Vendor> Vendor { get; set; }
    public DateTime ValidUntil { get; set; }
    public double Price { get; set; }
    public double Tax { get; set; }

    public Key(string value, string name, User createdBy, List<string> licensedFor, DateTime validUntil, double price,
        double tax)
    {
        this.ValidUntil = validUntil;
        this.Value = value;
        this.CreatedBy = createdBy;
        this.LicensedFor = licensedFor;
        this.Price = price;
        this.Name = name;
        this.Active = true;
        this.Tax = tax;
    }
}