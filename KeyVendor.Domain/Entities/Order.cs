using System.Runtime.CompilerServices;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Entities;

namespace KeyVendor.Domain.Entities;

public class Order : BaseEntity
{
    public User Buyer { get; set; }
    public Many<Key> Keys { get; set; }
    public double TotalPrice { get; set; }
    public OrderStatus Status { get; set; }

    public User Seller { get; set; }
    
    public Order(double totalPrice, User buyer, Many<Key> keys, User seller)
    {
        this.TotalPrice = totalPrice;
        this.Buyer = buyer;
        this.Keys = keys;
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