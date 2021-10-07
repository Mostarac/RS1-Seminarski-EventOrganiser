using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace webapp.Models
{
    public class Wallet

    {
        public int Id { get; set; }

        public string AppUserId { get; set; }

        [ForeignKey("AppUserId")]
        public AppUser AppUser { get; set; }

        public float Credits { get; set; }

        public int Discount { get; set; }
    }
}
