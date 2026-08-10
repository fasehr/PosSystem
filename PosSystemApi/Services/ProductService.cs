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

        public IReadOnlyList<Product> GetAllProducts()
        {
            return _context.Products.ToList();
        }

        public Product? FindProductBySku(string sku)
        {
            return _context.Products
                .FirstOrDefault(p => p.Sku == sku);
        }

        public void AddProduct(Product product)
        {
            if (product == null)
            {
                throw new ArgumentNullException(nameof(product));
            }

            if (_context.Products.Any(p => p.Sku == product.Sku))
            {
                throw new ArgumentException(
                    "A product with this SKU already exists.");
            }

            _context.Products.Add(product);
            _context.SaveChanges();
        }

        public void RemoveProduct(string sku)
        {
            var product = _context.Products
                .FirstOrDefault(p => p.Sku == sku);

            if (product == null)
            {
                throw new KeyNotFoundException("Product not found.");
            }

            _context.Products.Remove(product);
            _context.SaveChanges();
        }
    }
}