using PosSystemApi.Models;

namespace PosSystemApi.Services
{
    public interface ICheckoutService
    {
        Task<Order> CheckoutAsync(Cart cart);

        Task<List<Order>> GetOrdersAsync();
    }
}