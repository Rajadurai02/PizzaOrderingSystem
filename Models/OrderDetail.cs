using System;
using System.Collections.Generic;

#nullable disable

namespace PizzaOrderingApplication.Models
{
    public partial class OrderDetail
    {
        public OrderDetail()
        {
            OrderItemDetails = new HashSet<OrderItemDetail>();
        }

        public int ItemNumber { get; set; }
        public int? OrderId { get; set; }
        public int? PizzaNumber { get; set; }

        public virtual Order Order { get; set; }
        public virtual PizzaDetail PizzaNumberNavigation { get; set; }
        public virtual ICollection<OrderItemDetail> OrderItemDetails { get; set; }
    }
}
