using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace webapp.Models
{
    public class EventChannel
    {
        public int Id { get; set; }

        [Required]
        public int EventId { get; set; }

        [Required]
        public int ChannelId { get; set; }

        [Required]
        public string Link { get; set; }

        [ForeignKey("EventId")]
        public Event Event { get; set; }

        [ForeignKey("ChannelId")]
        public Channel Channel { get; set; }
    }
}
