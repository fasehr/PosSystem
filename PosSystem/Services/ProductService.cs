using PosSystem.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PosSystem.Services
{
    public class ProductService
    {
        private readonly List<Product> _products;

        private readonly Dictionary<string, Product> _productsBySku;

        public ProductService()
        {
            _products = new List<Product>();

            _productsBySku = new Dictionary<string, Product>();
        }

        public void AddProduct(Product product)
        {
            if (product == null)
            {
                throw new ArgumentNullException(nameof(product));
            }

            if (_productsBySku.ContainsKey(product.Sku))
            {
                throw new ArgumentException(
                    "A product with this SKU already exists.");
            }

            _products.Add(product);

            _productsBySku.Add(product.Sku, product);
        }

        public Product? FindProductBySku(string sku)
        {
            if (string.IsNullOrWhiteSpace(sku))
            {
                throw new ArgumentException("SKU cannot be empty.");
            }

            if (_productsBySku.TryGetValue(sku, out Product? product))
            {
                return product;
            }

            return null;
        }

        public IReadOnlyList<Product> GetAllProducts()
        {
            return _products;
        }
        public void RemoveProduct(string sku)
        {
            if (string.IsNullOrWhiteSpace(sku))
            {
                throw new ArgumentException("SKU cannot be empty.");
            }

            if (!_productsBySku.TryGetValue(sku, out Product? product))
            {
                throw new KeyNotFoundException(
                    "No product was found with this SKU.");
            }

            _products.Remove(product);
            _productsBySku.Remove(sku);
        }
    }
}
