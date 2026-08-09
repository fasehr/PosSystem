namespace PosSystemApi.Models
{
    public class CartItem
    {
        public Product Product { get; set; }

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
                    throw new ArgumentException(
                        "Quantity must be greater than zero.");
                }

                _quantity = value;
            }
        }

        public decimal LineTotal
        {
            get
            {
                return Product.UnitPrice * Quantity;
            }
        }

        public CartItem(Product product, int quantity)
        {
            Product = product;
            Quantity = quantity;
        }
    }
}