using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PosSystemApi.Models
{
    public class CartItem
    {
        [Key]
        public int CartItemId { get; set; }

        public string ProductSku { get; set; } = string.Empty;

        public Product Product { get; set; } = null!;

        public int Quantity { get; set; }

        public int? CartId { get; set; }

        public Cart? Cart { get; set; }

        public int? OrderId { get; set; }

        public Order? Order { get; set; }

        [NotMapped]
        public decimal LineTotal
        {
            get
            {
                return Product.UnitPrice * Quantity;
            }
        }

        public CartItem()
        {
        }

        public CartItem(Product product, int quantity)
        {
            Product = product;
            ProductSku = product.Sku;
            Quantity = quantity;
        }
    }
}