using System;
using System.Collections.Generic;

#nullable disable

namespace PizzaOrderingApplication.Models
{
    public partial class Topping
    {
        public Topping()
        {
            OrderItemDetails = new HashSet<OrderItemDetail>();
        }

        public int ToppingNumber { get; set; }
        public string ToppingName { get; set; }
        public int? ToppingPrice { get; set; }

        public virtual ICollection<OrderItemDetail> OrderItemDetails { get; set; }
    }
}
