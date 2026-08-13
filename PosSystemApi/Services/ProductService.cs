using Microsoft.EntityFrameworkCore;
using PosSystemApi.Data;
using PosSystemApi.Models;

namespace PosSystemApi.Services
{
    public class ProductService : IProductService
    {
        private readonly AppDbContext _context;

        public ProductService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Product>> GetAllProductsAsync()
        {
            return await _context.Products.ToListAsync();
        }

        public async Task<Product?> FindProductBySkuAsync(string sku)
        {
            return await _context.Products
                .FirstOrDefaultAsync(p => p.Sku == sku);
        }

        public async Task AddProductAsync(Product product)
        {
            if (product == null)
            {
                throw new ArgumentNullException(nameof(product));
            }

            bool exists = await _context.Products
                .AnyAsync(p => p.Sku == product.Sku);

            if (exists)
            {
                throw new ArgumentException(
                    "A product with this SKU already exists.");
            }

            _context.Products.Add(product);

            await _context.SaveChangesAsync();
        }

        public async Task RemoveProductAsync(string sku)
        {
            Product? product = await _context.Products
                .FirstOrDefaultAsync(p => p.Sku == sku);

            if (product == null)
            {
                throw new KeyNotFoundException(
                    "Product not found.");
            }

            _context.Products.Remove(product);

            await _context.SaveChangesAsync();
        }
    }
}