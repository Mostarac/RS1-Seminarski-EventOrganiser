using System;
using System.ComponentModel.DataAnnotations;
using webapp.Models;

namespace webapp.ViewModels
{
    public class EventIndexVmEM
    {
        public int Id { get; set; }

        [MaxLength(128)]
        public string Name { get; set; }

        public string ImageUrl { get; set; }

        [MaxLength(128)]
        public string Venue { get; set; }

        [MaxLength(64)]
        public string VenueType { get; set; }

        public string StreetAddress { get; set; }

        [MaxLength(64)]
        public string City { get; set; }

        [MaxLength(64)]
        public string Country { get; set; }

        [MaxLength(64)]
        public string EventType { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public string Description { get; set; }

    }
}
