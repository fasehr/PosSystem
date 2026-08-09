namespace PosSystemApi.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        // The list of items in the order 1 to many relationship with CartItem
        public List<CartItem> Items { get; set; }

        public decimal Total { get; set; }

        public DateTime CreatedAt { get; set; }
        // Constructor to initialize the order with items and total
        public Order(
            int orderId,
            List<CartItem> items,
            decimal total)
        {
            OrderId = orderId;
            Items = new List<CartItem>(items);
            Total = total;
            // Set the creation date to the current date and time
            CreatedAt = DateTime.Now;
        }
    }
}