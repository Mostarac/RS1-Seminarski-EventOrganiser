using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace webapp.ViewModels
{
    public class EventAddVmEM
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string ImageUrl { get; set; }

        [Required]
        public int VenueID { get; set; }

        [Required]
        public int EventTypeID { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        public string Description { get; set; }
        public IFormFile Picture { get; set; }

    }
}