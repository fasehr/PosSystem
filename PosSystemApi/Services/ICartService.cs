using PosSystemApi.Models;

namespace PosSystemApi.Services
{
    public interface ICartService
    {
        Task<Cart> GetCartAsync();

        Task AddToCartAsync(
            Product product,
            int quantity);

        Task UpdateQuantityAsync(
            string sku,
            int quantity);

        Task RemoveFromCartAsync(string sku);

        Task ClearCartAsync();

        Task UndoLastAddAsync();
    }
}