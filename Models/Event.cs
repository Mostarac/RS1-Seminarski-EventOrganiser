using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace webapp.Models
{
    public class Event : Base
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(128)]
        public string Name { get; set; }

        public string ImageUrl { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        public string Description { get; set; }

        public double? AverageRating { get; set; }

        public string TicketLink { get; set; }

        public int VenueId { get; set; }

        public int EventTypeId { get; set; }

        [ForeignKey("VenueId")]
        public Venue Venue { get; set; }

        [ForeignKey("EventTypeId")]
        public EventType EventType { get; set; }
    }
}
