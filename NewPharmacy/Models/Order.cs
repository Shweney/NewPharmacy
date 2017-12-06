using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace NewPharmacy.Models
{
    public class Order
    {
        public int Id { get; set; }

        [ScaffoldColumn(false)]
        public ApplicationUser Customer { get; set; }

        [ScaffoldColumn(false)]
        public ICollection<Cart> CartSet { get; set; }

        [DataType(DataType.Currency)]
        public Decimal SubTotal { get; set; }

        [ScaffoldColumn(false)]
        public bool Delivered { get; set; }

        [ScaffoldColumn(false)]
        [DataType(DataType.Date), Required]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime OrderedDate { get; set; }
    }
}