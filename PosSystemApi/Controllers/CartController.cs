using Microsoft.AspNetCore.Mvc;
using PosSystemApi.Models;
using PosSystemApi.Services;

namespace PosSystemApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CartController : ControllerBase
    {
        private readonly ICartService _cartService;
        private readonly IProductService _productService;

        public CartController(
            ICartService cartService,
            IProductService productService)
        {
            _cartService = cartService;
            _productService = productService;
        }

        // GET: api/cart
        [HttpGet]
        public async Task<IActionResult> GetCart()
        {
            Cart cart = await _cartService.GetCartAsync();

            return Ok(cart);
        }

        // POST: api/cart/{sku}/{quantity}
        [HttpPost("{sku}/{quantity}")]
        public async Task<IActionResult> AddToCart(
            string sku,
            int quantity)
        {
            try
            {
                Product? product =
                    await _productService.FindProductBySkuAsync(sku);

                if (product == null)
                {
                    return NotFound("Product not found.");
                }

                await _cartService.AddToCartAsync(
                    product,
                    quantity);

                Cart cart =
                    await _cartService.GetCartAsync();

                return Ok(cart);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT: api/cart/{sku}/{quantity}
        [HttpPut("{sku}/{quantity}")]
        public async Task<IActionResult> UpdateQuantity(
            string sku,
            int quantity)
        {
            try
            {
                await _cartService.UpdateQuantityAsync(
                    sku,
                    quantity);

                Cart cart =
                    await _cartService.GetCartAsync();

                return Ok(cart);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE: api/cart/{sku}
        [HttpDelete("{sku}")]
        public async Task<IActionResult> RemoveFromCart(
            string sku)
        {
            try
            {
                await _cartService.RemoveFromCartAsync(sku);

                Cart cart =
                    await _cartService.GetCartAsync();

                return Ok(cart);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE: api/cart
        [HttpDelete]
        public async Task<IActionResult> ClearCart()
        {
            await _cartService.ClearCartAsync();

            return Ok("Cart cleared successfully.");
        }

        // POST: api/cart/undo
        [HttpPost("undo")]
        public async Task<IActionResult> UndoLastAdd()
        {
            try
            {
                await _cartService.UndoLastAddAsync();

                Cart cart =
                    await _cartService.GetCartAsync();

                return Ok(cart);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}