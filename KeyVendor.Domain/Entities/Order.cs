using System.Runtime.CompilerServices;
using MongoDB.Entities;

namespace KeyVendor.Domain.Entities;

public class Order : BaseEntity
{
    public One<User> User { get; set; }
    public Many<Key> Keys { get; set; }
    public double TotalPrice { get; set; }
    public OrderStatus Status { get; set; }

    public One<User> Seller { get; set; }

    public Order(double totalPrice, One<User> user, Many<Key> keys, One<User> seller)
    {
        this.TotalPrice = totalPrice;
        this.User = user;
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