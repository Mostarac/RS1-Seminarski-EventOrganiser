using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace webapp.Models
{
    public class EventGear
    {

        public int Id { get; set; }

        public DateTime DateAdded { get; set; }

        public int EventId { get; set; }

        [ForeignKey("EventId")]
        public Event Event { get; set; }

        public int GearId { get; set; }

        [ForeignKey("GearId")]
        public Gear Gear { get; set; }

        public int? PurchaseId { get; set; }

        [ForeignKey("PurchaseId")]
        public Purchase Purchase { get; set; }
    }
}
