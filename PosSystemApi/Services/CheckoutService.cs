using PosSystemApi.Models;

namespace PosSystemApi.Services
{
    public class CheckoutService : ICheckoutService
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

        public IReadOnlyCollection<Order> GetOrders()
        {
            return _orders;
        }
    }
}