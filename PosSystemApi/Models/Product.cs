namespace PosSystemApi.Models
{
    public class Product
    {
        // Private fields
        private string _sku = string.Empty;
        private string _name = string.Empty;
        private string _category = string.Empty;
        private decimal _unitPrice;
        private int _stockQuantity;

        // Properties with validation
        public string Sku
        {
            get
            {
                return _sku;
            }

            set
            {  // Validate that the SKU is not empty or whitespace
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("SKU cannot be empty.");
                }

                _sku = value;
            }
        }

        public string Name
        {
            get
            {
                return _name;
            }

            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Name cannot be empty.");
                }

                _name = value;
            }
        }

        public string Category
        {
            get
            {
                return _category;
            }

            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Category cannot be empty.");
                }

                _category = value;
            }
        }

        public decimal UnitPrice
        {
            get
            {
                return _unitPrice;
            }

            set
            {
                if (value < 0)
                {
                    throw new ArgumentException(
                        "Unit price cannot be negative.");
                }

                _unitPrice = value;
            }
        }

        public int StockQuantity
        {
            get
            {
                return _stockQuantity;
            }

            set
            {
                if (value < 0)
                {
                    throw new ArgumentException(
                        "Stock quantity cannot be negative.");
                }

                _stockQuantity = value;
            }
        }
        // Constructor
        public Product(
            string sku,
            string name,
            decimal unitPrice,
            string category,
            int stockQuantity)
        {
            Sku = sku;
            Name = name;
            UnitPrice = unitPrice;
            Category = category;
            StockQuantity = stockQuantity;
        }
    }
}