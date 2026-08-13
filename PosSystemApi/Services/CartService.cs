using Microsoft.EntityFrameworkCore;
using PosSystemApi.Data;
using PosSystemApi.Models;

namespace PosSystemApi.Services
{
    public class CartService : ICartService
    {
        private readonly AppDbContext _context;

        public CartService(AppDbContext context)
        {
            _context = context;
        }

        private Cart GetOrCreateCart()
        {
            Cart? cart = _context.Carts
                .Include(c => c.Items)
                .ThenInclude(i => i.Product)
                .FirstOrDefault();

            if (cart == null)
            {
                cart = new Cart();

                _context.Carts.Add(cart);
                _context.SaveChanges();
            }

            return cart;
        }

        public void AddToCart(Product product, int quantity)
        {
            if (product == null)
            {
                throw new ArgumentNullException(nameof(product));
            }

            if (quantity <= 0)
            {
                throw new ArgumentException(
                    "Quantity must be greater than zero.");
            }

            if (quantity > product.StockQuantity)
            {
                throw new InvalidOperationException(
                    "Requested quantity exceeds available stock.");
            }

            Cart cart = GetOrCreateCart();

            CartItem? existingItem = cart.Items
                .FirstOrDefault(i => i.ProductSku == product.Sku);

            if (existingItem != null)
            {
                int newQuantity = existingItem.Quantity + quantity;

                if (newQuantity > product.StockQuantity)
                {
                    throw new InvalidOperationException(
                        "Requested quantity exceeds available stock.");
                }

                existingItem.Quantity = newQuantity;

                _context.SaveChanges();
                return;
            }

            CartItem newItem = new CartItem(product, quantity);

            newItem.CartId = cart.CartId;

            cart.Items.Add(newItem);

            _context.SaveChanges();
        }

        public void RemoveFromCart(string sku)
        {
            if (string.IsNullOrWhiteSpace(sku))
            {
                throw new ArgumentException(
                    "SKU cannot be empty.");
            }

            Cart cart = GetOrCreateCart();

            CartItem? item = cart.Items
                .FirstOrDefault(i => i.ProductSku == sku);

            if (item == null)
            {
                throw new KeyNotFoundException(
                    "Product not found in cart.");
            }

            _context.CartItems.Remove(item);

            _context.SaveChanges();
        }

        public void UpdateQuantity(string sku, int quantity)
        {
            if (string.IsNullOrWhiteSpace(sku))
            {
                throw new ArgumentException(
                    "SKU cannot be empty.");
            }

            if (quantity <= 0)
            {
                throw new ArgumentException(
                    "Quantity must be greater than zero.");
            }

            Cart cart = GetOrCreateCart();

            CartItem? item = cart.Items
                .FirstOrDefault(i => i.ProductSku == sku);

            if (item == null)
            {
                throw new KeyNotFoundException(
                    "Product not found in cart.");
            }

            if (quantity > item.Product.StockQuantity)
            {
                throw new InvalidOperationException(
                    "Requested quantity exceeds available stock.");
            }

            item.Quantity = quantity;

            _context.SaveChanges();
        }

        public void ClearCart()
        {
            Cart cart = GetOrCreateCart();

            _context.CartItems.RemoveRange(cart.Items);

            _context.SaveChanges();
        }

        public Cart GetCart()
        {
            return GetOrCreateCart();
        }

        public void UndoLastAdd()
        {
            throw new NotImplementedException(
                "Undo will be converted to persistent storage separately.");
        }
    }
}