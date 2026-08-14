using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PosSystemApi.DTOs;
using PosSystemApi.Models;
using PosSystemApi.Services;

namespace PosSystemApi.Controllers
{
    [Authorize(Roles = "Admin,Salesman")]
    [ApiController]
    [Route("api/[controller]")]
    public class CheckoutController : ControllerBase
    {
        private readonly ICartService _cartService;
        private readonly ICheckoutService _checkoutService;

        public CheckoutController(
            ICartService cartService,
            ICheckoutService checkoutService)
        {
            _cartService = cartService;
            _checkoutService = checkoutService;
        }

        // POST: api/checkout
        [HttpPost]
        public async Task<IActionResult> Checkout(
            CheckoutRequest request)
        {
            try
            {
                Cart cart =
                    await _cartService.GetCartAsync();

                Order order =
                    await _checkoutService.CheckoutAsync(
                        cart,
                        request.CustomerId,
                        request.PaymentType);

                return Ok(order);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}