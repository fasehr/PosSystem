using PosSystemApi.Models;

namespace PosSystemApi.Services
{
    public interface IProductService
    {
        IReadOnlyList<Product> GetAllProducts();
        Product? FindProductBySku(string sku);
        void AddProduct(Product product);
        void RemoveProduct(string sku);
    }
}