using PosSystemApi.Models;

namespace PosSystemApi.Services
{
    public interface IProductService
    {
        Task<List<Product>> GetAllProductsAsync(
            string? category,
            bool? inStock,
            decimal? minPrice,
            decimal? maxPrice);

        Task<Product?> FindProductBySkuAsync(string sku);

        Task AddProductAsync(Product product);

        Task RemoveProductAsync(string sku);
    }
}