using Newtonsoft.Json;
using PosSystem.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PosSystem.Services
{
    public class CheckoutService
    {
        private readonly Queue<Order> _orders;
        private int _nextOrderId;

        public CheckoutService()
        {
            _orders = new Queue<Order>();
            _nextOrderId = 1;
        }

        public Order Checkout(Cart cart)
        {
            if (cart == null)
            {
                throw new ArgumentNullException(nameof(cart));
            }

            if (cart.Items.Count == 0)
            {
                throw new InvalidOperationException("Cart is empty.");
            }

            Order order = new Order(
                _nextOrderId++,
                cart.Items,
                cart.Total);

            foreach (CartItem item in cart.Items)
            {
                item.Product.StockQuantity -= item.Quantity;
            }

            _orders.Enqueue(order);

            cart.Items.Clear();

            return order;
        }

        public void DisplayReceipt(Order order)
        {
            if (order == null)
            {
                throw new ArgumentNullException(nameof(order));
            }

            StringBuilder receipt = new StringBuilder();

            receipt.AppendLine();
            receipt.AppendLine("========== RECEIPT ==========");
            receipt.AppendLine($"Order ID: {order.OrderId}");
            receipt.AppendLine($"Date: {order.CreatedAt}");
            receipt.AppendLine("-----------------------------");

            foreach (CartItem item in order.Items)
            {
                receipt.AppendLine(
                    $"{item.Product.Name} x{item.Quantity} = £{item.LineTotal:N2}");
            }

            receipt.AppendLine("-----------------------------");
            receipt.AppendLine($"TOTAL: £{order.Total:N2}");
            receipt.AppendLine("=============================");

            Console.WriteLine(receipt.ToString());
        }

        public void DisplayOrderAsJson(Order order)
        {
            Console.WriteLine("\n===== ORDER IN JSON FORMAT =====");

            string json = JsonConvert.SerializeObject(
                order,
                Formatting.Indented);

            Console.WriteLine(json);
        }

        public IReadOnlyCollection<Order> GetOrders()
        {
            return _orders;
        }
    }
}