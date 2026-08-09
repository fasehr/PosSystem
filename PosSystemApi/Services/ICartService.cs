using PosSystemApi.Models;

namespace PosSystemApi.Services
{
    public interface ICartService
    {
        Cart GetCart();
        void AddToCart(Product product, int quantity);
        void UpdateQuantity(string sku, int quantity);
        void RemoveFromCart(string sku);
        void ClearCart();
        void UndoLastAdd();
    }
}