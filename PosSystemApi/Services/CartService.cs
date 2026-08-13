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

        private async Task<Cart> GetOrCreateCartAsync()
        {
            Cart? cart = await _context.Carts
                .Include(c => c.Items)
                .ThenInclude(i => i.Product)
                .FirstOrDefaultAsync();

            if (cart == null)
            {
                cart = new Cart();

                await _context.Carts.AddAsync(cart);
                await _context.SaveChangesAsync();
            }

            return cart;
        }

        public async Task AddToCartAsync(Product product, int quantity)
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

            Cart cart = await GetOrCreateCartAsync();

            CartItem? existingItem = cart.Items
                .FirstOrDefault(i => i.ProductSku == product.Sku);

            if (existingItem != null)
            {
                int newQuantity =
                    existingItem.Quantity + quantity;

                if (newQuantity > product.StockQuantity)
                {
                    throw new InvalidOperationException(
                        "Requested quantity exceeds available stock.");
                }

                existingItem.Quantity = newQuantity;

                await _context.SaveChangesAsync();
                return;
            }

            CartItem newItem =
                new CartItem(product, quantity);

            newItem.CartId = cart.CartId;

            cart.Items.Add(newItem);

            await _context.SaveChangesAsync();
        }

        public async Task RemoveFromCartAsync(string sku)
        {
            if (string.IsNullOrWhiteSpace(sku))
            {
                throw new ArgumentException(
                    "SKU cannot be empty.");
            }

            Cart cart = await GetOrCreateCartAsync();

            CartItem? item = cart.Items
                .FirstOrDefault(i => i.ProductSku == sku);

            if (item == null)
            {
                throw new KeyNotFoundException(
                    "Product not found in cart.");
            }

            _context.CartItems.Remove(item);

            await _context.SaveChangesAsync();
        }

        public async Task UpdateQuantityAsync(
            string sku,
            int quantity)
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

            Cart cart = await GetOrCreateCartAsync();

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

            await _context.SaveChangesAsync();
        }

        public async Task ClearCartAsync()
        {
            Cart cart = await GetOrCreateCartAsync();

            _context.CartItems.RemoveRange(cart.Items);

            await _context.SaveChangesAsync();
        }

        public async Task<Cart> GetCartAsync()
        {
            return await GetOrCreateCartAsync();
        }

        public Task UndoLastAddAsync()
        {
            throw new NotImplementedException(
                "Undo will be converted to persistent storage separately.");
        }
    }
}