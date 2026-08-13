using PosSystemApi.Models;
using System.ComponentModel.DataAnnotations;

public class Order
{
    [Key]
    public int OrderId { get; set; }

    public List<CartItem> Items { get; set; } = new();

    public decimal Total { get; set; }

    public PaymentType PaymentMethod { get; set; }
    public DateTime CreatedAt { get; set; }

    public int CustomerId { get; set; }

    public Customer Customer { get; set; } = null!;

    public Order()
    {
    }

    public Order(
     int orderId,
     List<CartItem> items,
     decimal total,
     PaymentType paymentMethod)
    {
        OrderId = orderId;
        Items = new List<CartItem>(items);
        Total = total;
        PaymentMethod = paymentMethod;
        CreatedAt = DateTime.Now;
    }
}