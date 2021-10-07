using System;
using System.Linq;

namespace webapp.ViewModels
{
    public class EventVm
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string ImageUrl { get; set; }

        public string Address { get; set; }

        public string Venue { get; set; }

        public string EventType { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public string Description { get; set; }

        public int Capacity { get; set; }
        
        public string Street { get; set; }
        
        public string City { get; set; }

        public string Country { get; set; }

        public string TicketLink { get; set; }

        public string Message { get; set; }

        public int CountOfComments { get; set; }

        public double? Rating { get; set; }

        public string CreatedBy { get; set; }

        public IQueryable<EventVm> SimilarEvents { get; set; }
    }
}
