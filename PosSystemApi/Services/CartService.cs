using PosSystemApi.Models;

namespace PosSystemApi.Services
{
    public class CartService : ICartService
    {
        private readonly Cart _cart;
        private readonly Stack<CartItem> _undoStack;

        public CartService()
        {
            _cart = new Cart();
            _undoStack = new Stack<CartItem>();
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

            foreach (CartItem item in _cart.Items)
            {
                if (item.Product.Sku == product.Sku)
                {
                    int newQuantity = item.Quantity + quantity;

                    if (newQuantity > product.StockQuantity)
                    {
                        throw new InvalidOperationException(
                            "Requested quantity exceeds available stock.");
                    }

                    item.Quantity = newQuantity;

                    _undoStack.Push(
                        new CartItem(product, quantity));

                    return;
                }
            }

            CartItem newItem = new CartItem(product, quantity);

            _cart.Items.Add(newItem);

            _undoStack.Push(
                new CartItem(product, quantity));
        }

        public void UndoLastAdd()
        {
            if (_undoStack.Count == 0)
            {
                throw new InvalidOperationException(
                    "There is no cart action to undo.");
            }

            CartItem lastAddedItem = _undoStack.Pop();
            CartItem? itemToUndo = null;

            foreach (CartItem item in _cart.Items)
            {
                if (item.Product.Sku == lastAddedItem.Product.Sku)
                {
                    itemToUndo = item;
                    break;
                }
            }

            if (itemToUndo == null)
            {
                throw new InvalidOperationException(
                    "The last added product is no longer in the cart.");
            }

            if (itemToUndo.Quantity > lastAddedItem.Quantity)
            {
                itemToUndo.Quantity -= lastAddedItem.Quantity;
            }
            else
            {
                _cart.Items.Remove(itemToUndo);
            }
        }

        public void RemoveFromCart(string sku)
        {
            if (string.IsNullOrWhiteSpace(sku))
            {
                throw new ArgumentException("SKU cannot be empty.");
            }

            CartItem? itemToRemove = null;

            foreach (CartItem item in _cart.Items)
            {
                if (item.Product.Sku == sku)
                {
                    itemToRemove = item;
                    break;
                }
            }

            if (itemToRemove == null)
            {
                throw new KeyNotFoundException(
                    "Product not found in cart.");
            }

            _cart.Items.Remove(itemToRemove);
        }

        public void UpdateQuantity(string sku, int quantity)
        {
            if (string.IsNullOrWhiteSpace(sku))
            {
                throw new ArgumentException("SKU cannot be empty.");
            }

            if (quantity <= 0)
            {
                throw new ArgumentException(
                    "Quantity must be greater than zero.");
            }

            foreach (CartItem item in _cart.Items)
            {
                if (item.Product.Sku == sku)
                {
                    if (quantity > item.Product.StockQuantity)
                    {
                        throw new InvalidOperationException(
                            "Requested quantity exceeds available stock.");
                    }

                    item.Quantity = quantity;
                    return;
                }
            }

            throw new KeyNotFoundException(
                "Product not found in cart.");
        }

        public void ClearCart()
        {
            _cart.Items.Clear();
            _undoStack.Clear();
        }

        public Cart GetCart()
        {
            return _cart;
        }
    }
}