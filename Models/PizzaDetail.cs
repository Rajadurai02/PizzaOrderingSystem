using System;
using System.Collections.Generic;

#nullable disable

namespace PizzaOrderingApplication.Models
{
    public partial class PizzaDetail
    {
        public PizzaDetail()
        {
            OrderDetails = new HashSet<OrderDetail>();
        }

        public int PizzaNumber { get; set; }
        public string PizzaName { get; set; }
        public int? PizzaPrice { get; set; }
        public string PizzaType { get; set; }

        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
