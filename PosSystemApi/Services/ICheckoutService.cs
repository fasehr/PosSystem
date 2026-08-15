using PosSystemApi.Models;

namespace PosSystemApi.Services
{
    public interface ICheckoutService
    {
        Task<Order> CheckoutAsync(
            Cart cart,
            int customerId,
            PaymentType paymentType);

        Task<List<Order>> GetOrdersAsync(
            int? customerId,
            PaymentType? paymentType,
            DateTime? fromDate,
            DateTime? toDate);
    }
}