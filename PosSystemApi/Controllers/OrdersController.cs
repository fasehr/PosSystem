using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
        public async Task<IActionResult> GetOrders()
        {
            var orders =
                await _checkoutService.GetOrdersAsync();

            return Ok(orders);
        }
    }
}