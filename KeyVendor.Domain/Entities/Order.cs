using System.Runtime.CompilerServices;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Entities;

namespace KeyVendor.Domain.Entities;

public class Order : BaseEntity
{
    public User Buyer { get; set; }
    public List<Key> Keys { get; set; }
    public double TotalPrice { get; set; }
    public OrderStatus Status { get; set; }

    public User Seller { get; set; }

    public Order(double totalPrice, User buyer, User seller)
    {
        this.TotalPrice = totalPrice;
        this.Buyer = buyer;
        this.Status = OrderStatus.Pending;
        this.Seller = seller;
    }
}

public enum OrderStatus
{
    Pending,
    Cancelled,
    Completed
}