namespace PosSystem.Models
{
    public class CartItem
    {
        // The product added to the cart.
        public Product Product { get; set; }             // composition relationship with Product class       

        // Number of units of this product.
        private int _quantity;

        public int Quantity
        {
            get
            {
                return _quantity;
            }

            set
            {
                if (value <= 0)
                {
                    throw new ArgumentException("Quantity must be greater than zero.");
                }

                _quantity = value;
            }
        }

        // Total cost for this cart item.
        public decimal LineTotal
        {
            get
            {
                return Product.UnitPrice * Quantity;
            }
        }
        // Constructor to initialize a new instance of the CartItem class.
        public CartItem(Product product, int quantity)
        {
            Product = product;
            Quantity = quantity;
        }
    }
}
