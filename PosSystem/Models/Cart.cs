using System;
using System.Collections.Generic;

namespace PosSystem.Models
{
    public class Cart
    {
        // A list of items in the cart. Each item is represented by a CartItem object. 1 to many relationship with CartItem class.
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
            // // Initialize the cart with an empty list of items.
            Items = new List<CartItem>();
        }
    }
}

