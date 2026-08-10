using Microsoft.EntityFrameworkCore;
using PosSystemApi.Data;
using PosSystemApi.Models;

namespace PosSystemApi.Services
{
    public class CheckoutService : ICheckoutService
    {
        private readonly AppDbContext _context;

        public CheckoutService(AppDbContext context)
        {
            _context = context;
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

            foreach (CartItem item in cart.Items)
            {
                if (item.Quantity > item.Product.StockQuantity)
                {
                    throw new InvalidOperationException(
                        $"Not enough stock for product {item.Product.Name}.");
                }
            }

            Order order = new Order
            {
                Total = cart.Total,
                CreatedAt = DateTime.Now
            };

            foreach (CartItem item in cart.Items)
            {
                item.Product.StockQuantity -= item.Quantity;

                item.CartId = null;
                item.Cart = null;

                order.Items.Add(item);
            }

            _context.Orders.Add(order);

            _context.SaveChanges();

            cart.Items.Clear();

            _context.SaveChanges();

            return order;
        }

        public IReadOnlyCollection<Order> GetOrders()
        {
            return _context.Orders
                .Include(o => o.Items)
                .ThenInclude(i => i.Product)
                .ToList();
        }
    }
}