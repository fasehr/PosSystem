using PosSystemApi.Models;

namespace PosSystemApi.DTOs
{
    public class CheckoutRequest
    {
        public int CustomerId { get; set; }

        public PaymentType PaymentType { get; set; }
    }
}