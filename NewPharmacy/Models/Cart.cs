using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace NewPharmacy.Models
{
    public class Cart
    {
        public int Id { get; set; }
        public ApplicationUser User { get; set; }

        public Product Product { get; set; }

        [DataType(DataType.Currency), Range(0, 3000)]
        public Decimal Price { get; set; }

        [Display(Name = "Quantity")]
        public int OrderQuantity { get; set; }

        [DataType(DataType.Currency), Range(0, 3000)]
        public Decimal Total { get; set; }

        [ScaffoldColumn(false)]
        public bool IsCheckedOut { get; set; }  
    }
}