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
        public IActionResult GetCart()
        {
            return Ok(_cartService.GetCart());
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

                _cartService.AddToCart(product, quantity);

                return Ok(_cartService.GetCart());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT: api/cart/{sku}/{quantity}
        [HttpPut("{sku}/{quantity}")]
        public IActionResult UpdateQuantity(
            string sku,
            int quantity)
        {
            try
            {
                _cartService.UpdateQuantity(sku, quantity);

                return Ok(_cartService.GetCart());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE: api/cart/{sku}
        [HttpDelete("{sku}")]
        public IActionResult RemoveFromCart(string sku)
        {
            try
            {
                _cartService.RemoveFromCart(sku);

                return Ok(_cartService.GetCart());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE: api/cart
        [HttpDelete]
        public IActionResult ClearCart()
        {
            _cartService.ClearCart();

            return Ok("Cart cleared successfully.");
        }

        // POST: api/cart/undo
        [HttpPost("undo")]
        public IActionResult UndoLastAdd()
        {
            try
            {
                _cartService.UndoLastAdd();

                return Ok(_cartService.GetCart());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}