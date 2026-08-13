using PosSystemApi.Models;

namespace PosSystemApi.Services
{
    public interface IProductService
    {
        Task<List<Product>> GetAllProductsAsync();
        Task<Product?> FindProductBySkuAsync(string sku);
        Task AddProductAsync(Product product);
        Task RemoveProductAsync(string sku);
    }
}