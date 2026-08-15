using PosSystemApi.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Order
{
    [Key]
    public int OrderId { get; set; }

    public List<CartItem> Items { get; set; } = new();

    [Column(TypeName = "decimal(18,2)")]
    public decimal Total { get; set; }

    public PaymentType PaymentType { get; set; }

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
        PaymentType paymentType)
    {
        OrderId = orderId;
        Items = new List<CartItem>(items);
        Total = total;
        PaymentType = paymentType;
        CreatedAt = DateTime.Now;
    }
}