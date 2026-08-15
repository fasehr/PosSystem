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
        //  This method retrieves a list of products from the database based on optional filtering criteria.
        public async Task<List<Product>> GetAllProductsAsync(
          string? category,
          bool? inStock,
          decimal? minPrice,
          decimal? maxPrice)
        {
            var query = _context.Products.AsQueryable();

            if (!string.IsNullOrWhiteSpace(category))
            {
                query = query.Where(p =>
                    p.Category == category);
            }

            if (inStock == true)
            {
                query = query.Where(p =>
                    p.StockQuantity > 0);
            }

            if (minPrice.HasValue)
            {
                query = query.Where(p =>
                    p.UnitPrice >= minPrice.Value);
            }

            if (maxPrice.HasValue)
            {
                query = query.Where(p =>
                    p.UnitPrice <= maxPrice.Value);
            }

            return await query.ToListAsync();
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