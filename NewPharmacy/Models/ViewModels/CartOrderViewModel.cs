using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NewPharmacy.Models.ViewModels
{
    public class CartOrderViewModel
    {
        public ICollection<Cart> PendingItems { get; set; }
        public ICollection<Order> PendingOrders { get; set; }
        public decimal ItemsSubTotal { get; set; }
    }
}