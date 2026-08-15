using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PosSystemApi.Models;
using PosSystemApi.Services;

namespace PosSystemApi.Controllers
{
    [Authorize(Roles = "Admin,Salesman")]
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly ICheckoutService _checkoutService;

        public OrdersController(
            ICheckoutService checkoutService)
        {
            _checkoutService = checkoutService;
        }

        // GET: api/orders
        [HttpGet]
        public async Task<IActionResult> GetOrders(
        int? customerId,
        PaymentType? paymentType,
        DateTime? fromDate,
        DateTime? toDate)
        {
            var orders = await _checkoutService.GetOrdersAsync(
                customerId,
                paymentType,
                fromDate,
                toDate);

            return Ok(orders);
        }
    }
}