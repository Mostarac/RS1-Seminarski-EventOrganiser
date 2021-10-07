using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using webapp.Models;
using Microsoft.AspNetCore.Http;

namespace webapp.ViewModels
{
    public class VenueViewModel
    {
        
        public string Name { get; set; }

        public string ImageUrl { get; set; }
        [Range(1, int.MaxValue)]
        [Required]
        public int Capacity { get; set; }
        [Required]
        public int VenueTypeID { get; set; }
        [Required]
        public int StreetID { get; set; }
        public IFormFile Picture { get; set; }
    }
}
