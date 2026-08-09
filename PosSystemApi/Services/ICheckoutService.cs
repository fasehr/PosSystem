using PosSystemApi.Models;

namespace PosSystemApi.Services
{
    public interface ICheckoutService
    {
        Order Checkout(Cart cart);
        IReadOnlyCollection<Order> GetOrders();
    }
}