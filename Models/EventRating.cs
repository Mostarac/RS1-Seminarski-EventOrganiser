using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace webapp.Models
{
    public class EventRating : Base
    {
        [Key]
        public int Id { get; set; }

        public int Rate { get; set; }

        public string UserId { get; set; }

        public int EventId { get; set; }

        [ForeignKey("UserId")]
        public AppUser AppUser { get; set; }

        [ForeignKey("EventId")]
        public Event Event { get; set; }
    }
}
