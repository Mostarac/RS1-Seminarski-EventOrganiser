using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace webapp.Models
{
    public class VenueChannel
    {
        public int Id { get; set; }

        [Required]
        public int VenueId { get; set; }

        [Required]
        public int ChannelId { get; set; }

        [Required]
        public string Link { get; set; }

        [ForeignKey("VenueId")]
        public Venue Venue { get; set; }

        [ForeignKey("ChannelId")]
        public Channel Channel { get; set; }
    }
}
