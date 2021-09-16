using System;
using System.Collections.Generic;

#nullable disable

namespace PizzaOrderingApplication.Models
{
    public partial class OrderItemDetail
    {
        public int ItemNumber { get; set; }
        public int ToppingNumber { get; set; }

        public virtual OrderDetail ItemNumberNavigation { get; set; }
        public virtual Topping ToppingNumberNavigation { get; set; }
    }
}
