using Microsoft.AspNetCore.Mvc;
using PosSystemApi.DTOs;
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

        // POST: api/cart
        [HttpPost]
        public IActionResult AddToCart(AddCartItemRequest request)
        {
            try
            {
                Product? product =
                    _productService.FindProductBySku(request.Sku);

                if (product == null)
                {
                    return NotFound("Product not found.");
                }

                _cartService.AddToCart(product, request.Quantity);

                return Ok("Product added to cart.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT: api/cart/{sku}
        [HttpPut("{sku}")]
        public IActionResult UpdateQuantity(
            string sku,
            UpdateCartItemRequest request)
        {
            try
            {
                _cartService.UpdateQuantity(
                    sku,
                    request.Quantity);

                return Ok("Quantity updated.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE: api/cart/{sku}
        [HttpDelete("{sku}")]
        public IActionResult RemoveItem(string sku)
        {
            try
            {
                _cartService.RemoveFromCart(sku);

                return Ok("Item removed.");
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

            return Ok("Cart cleared.");
        }

        // POST: api/cart/undo
        [HttpPost("undo")]
        public IActionResult UndoLastAdd()
        {
            try
            {
                _cartService.UndoLastAdd();

                return Ok("Last action undone.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}