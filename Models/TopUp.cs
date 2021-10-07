using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace webapp.Models
{
    public class TopUp
    {
        public int Id { get; set; }

        public int TopUpCardId { get; set; }

        [ForeignKey("TopUpCardId")]
        public TopUpCard TopUpCard { get; set; }

        public string AppUserId { get; set; }

        [ForeignKey("AppUserId")]
        public AppUser AppUser { get; set; }

        public DateTime TopUpDate { get; set; }
    }
}
