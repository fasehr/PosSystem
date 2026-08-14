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

        public async Task<Order> CheckoutAsync(
            Cart cart,
            int customerId,
            PaymentType paymentType)
        {
            if (cart == null)
            {
                throw new ArgumentNullException(nameof(cart));
            }

            if (cart.Items.Count == 0)
            {
                throw new InvalidOperationException("Cart is empty.");
            }

            Customer? customer = await _context.Customers
                .FirstOrDefaultAsync(c => c.CustomerId == customerId);

            if (customer == null)
            {
                throw new KeyNotFoundException("Customer not found.");
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
                PaymentType = paymentType,
                CreatedAt = DateTime.Now,
                CustomerId = customerId,
                Customer = customer
            };

            foreach (CartItem item in cart.Items)
            {
                item.Product.StockQuantity -= item.Quantity;

                item.CartId = null;
                item.Cart = null;

                order.Items.Add(item);
            }

            await _context.Orders.AddAsync(order);

            await _context.SaveChangesAsync();

            cart.Items.Clear();

            await _context.SaveChangesAsync();

            return order;
        }

        public async Task<List<Order>> GetOrdersAsync()
        {
            return await _context.Orders
                .Include(o => o.Items)
                .ThenInclude(i => i.Product)
                .Include(o => o.Customer)
                .ToListAsync();
        }
    }
}