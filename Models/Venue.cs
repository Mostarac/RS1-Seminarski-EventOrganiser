using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace webapp.Models
{
    public class Venue
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(128)]
        public string Name { get; set; }

        public string ImageUrl { get; set; }

        [Required]
        public int Capacity { get; set; }

        public int VenueTypeId { get; set; }

        public int AddressId { get; set; }

        public string AppUserId { get; set; }

        [ForeignKey("AppUserId")]
        public AppUser AppUser { get; set; }

        [ForeignKey("VenueTypeId")]
        public VenueType VenueType { get; set; }

        [ForeignKey("AddressId")]
        public Address Address { get; set; }

    }
}
