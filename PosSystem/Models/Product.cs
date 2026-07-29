using System;
using System.Collections.Generic;
using System.Text;

namespace PosSystem.Models
{
    // This class represents a product in the POS system.
    public class Product
    {
        // Unique identifier for the product.
        private string _sku = "";
        // SKU property with validation to ensure it is not empty.
        public string Sku
        {
            get
            {
                return _sku;
            }

            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("SKU cannot be empty.");
                }

                _sku = value;
            }
        }
        // Name of the product.
        private string _name = "";
        // Name property with validation to ensure it is not empty.
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

        // Category of the product.
        private string _category = "";

        //  Category property with validation to ensure it is not empty.
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
        // Price of the product, with validation to ensure it is not negative.
        private decimal _unitPrice;
       
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
                    throw new ArgumentException("Unit price cannot be negative.");
                }

                _unitPrice = value;
            }
        }
        // Quantity of the product in stock, with validation to ensure it is not negative.
        private int _stockQuantity;

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
                    throw new ArgumentException("Stock quantity cannot be negative.");
                }

                _stockQuantity = value;
            }
        }
        // Constructor to initialize a new instance of the Product class.
        public Product(string sku,string name,decimal unitPrice,string category,int stockQuantity)
        {
            Sku = sku;
            Name = name;
            UnitPrice = unitPrice;
            Category = category;
            StockQuantity = stockQuantity;
        }

    }
}
