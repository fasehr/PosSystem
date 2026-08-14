using Microsoft.AspNetCore.Mvc;
using PosSystemApi.Models;
using PosSystemApi.Services;
using Microsoft.AspNetCore.Authorization;

namespace PosSystemApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        // GET: api/products
        [Authorize(Roles = "Admin,Salesman")]
        [HttpGet]
        public async Task<IActionResult> GetAllProducts()
        {
            var products =
                await _productService.GetAllProductsAsync();

            return Ok(products);
        }

        // GET: api/products/{sku}
        [Authorize(Roles = "Admin,Salesman")]
        [HttpGet("{sku}")]
        public async Task<IActionResult> GetProduct(string sku)
        {
            Product? product =
                await _productService.FindProductBySkuAsync(sku);

            if (product == null)
            {
                return NotFound("Product not found.");
            }

            return Ok(product);
        }

        // POST: api/products
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> AddProduct(Product product)
        {
            try
            {
                await _productService.AddProductAsync(product);

                return Ok(product);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE: api/products/{sku}
        [Authorize(Roles = "Admin")]
        [HttpDelete("{sku}")]
        public async Task<IActionResult> DeleteProduct(string sku)
        {
            try
            {
                await _productService.RemoveProductAsync(sku);

                return Ok("Product removed successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}