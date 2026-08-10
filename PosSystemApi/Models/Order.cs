using System.ComponentModel.DataAnnotations;

namespace PosSystemApi.Models
{
    public class Order
    {
        [Key]
        public int OrderId { get; set; }

        public List<CartItem> Items { get; set; } = new();

        public decimal Total { get; set; }

        public DateTime CreatedAt { get; set; }

        public Order()
        {
        }

        public Order(
            int orderId,
            List<CartItem> items,
            decimal total)
        {
            OrderId = orderId;
            Items = new List<CartItem>(items);
            Total = total;
            CreatedAt = DateTime.Now;
        }
    }
}