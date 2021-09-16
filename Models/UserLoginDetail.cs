using System;
using System.Collections.Generic;

#nullable disable

namespace PizzaOrderingApplication.Models
{
    public partial class UserLoginDetail
    {
        public UserLoginDetail()
        {
            Orders = new HashSet<Order>();
        }

        public int UserId { get; set; }
        public string UserMail { get; set; }
        public string UserName { get; set; }
        public string UserPassword { get; set; }
        public string UserAddress { get; set; }
        public string UserPhone { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}
