using System;
using System.Collections.Generic;

namespace PosSystem.Models
{
    public class Order
    {
        public int OrderId { get; set; }

        public List<CartItem> Items { get; set; }

        public decimal Total { get; set; }

        public DateTime CreatedAt { get; set; }

        public Order(int orderId, List<CartItem> items, decimal total)
        {
            OrderId = orderId;
            Items = new List<CartItem>(items);
            Total = total;
            CreatedAt = DateTime.Now;
        }
        
    }
}

