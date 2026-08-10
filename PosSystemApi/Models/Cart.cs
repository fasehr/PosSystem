using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PosSystemApi.Models
{
    public class Cart
    {
        [Key]
        public int CartId { get; set; }

        public List<CartItem> Items { get; set; } = new();

        [NotMapped]
        public decimal Total
        {
            get
            {
                decimal total = 0;

                foreach (CartItem item in Items)
                {
                    total += item.LineTotal;
                }

                return total;
            }
        }
    }
}