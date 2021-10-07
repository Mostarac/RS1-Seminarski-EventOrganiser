using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace webapp.Models
{
    public class Gear
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public float Price { get; set; }

        public int GearTypeId { get; set; }

        [ForeignKey("GearTypeId")]
        public GearType GearType { get; set; }

    }
}