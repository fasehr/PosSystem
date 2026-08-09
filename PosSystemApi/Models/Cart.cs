namespace PosSystemApi.Models
{
    public class Cart
    {
        public List<CartItem> Items { get; set; }

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

        public Cart()
        {
            Items = new List<CartItem>();
        }
    }
}