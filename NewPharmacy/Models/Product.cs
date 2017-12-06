using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace NewPharmacy.Models
{
    public class Product
    {
        public int Id { get; set; }

        [Required, Display(Name ="Product")]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public Category Category { get; set; }

        [DataType(DataType.Currency), Range(0, 300)]
        public Decimal Price { get; set; }

        [ScaffoldColumn(false)]
        public string ImagePath { get; set; }

        [ScaffoldColumn(false), Display(Name = "In Stock")]
        public int QuantityInStock { get; set; }
    }
}