using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PosSystemApi.Models
{
    public class Product
    {
        [Key]
        public string Sku { get; set; } = string.Empty;

        public string Name { get; set; } = string.Empty;

        public string Category { get; set; } = string.Empty;

        [Column(TypeName = "decimal(18,2)")]
        public decimal UnitPrice { get; set; }

        public int StockQuantity { get; set; }

        // EF Core needs an easy way to create the entity
        public Product()
        {
        }

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