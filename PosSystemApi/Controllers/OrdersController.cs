using Microsoft.AspNetCore.Mvc;
using PosSystemApi.Services;

namespace PosSystemApi.Controllers
{
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
        public IActionResult GetOrders()
        {
            return Ok(_checkoutService.GetOrders());
        }
    }
}