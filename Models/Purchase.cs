using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace webapp.Models
{
    public class Purchase
    {
        public int Id { get; set; }

        public float TotalPrice { get; set; }

        public float TotalDiscount { get; set; }

        public DateTime PurchaseDate { get; set; }

    }
}