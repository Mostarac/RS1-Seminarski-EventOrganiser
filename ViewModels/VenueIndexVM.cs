using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace webapp.ViewModels
{
    public class VenueIndexVM
    {
        public int VenueID { get; set; }

        [MaxLength(128)]
        public string Name { get; set; }

        public string ImageUrl { get; set; }

        public int Capacity { get; set; }

        [MaxLength(64)]
        public string Venuetype { get; set; }

        [MaxLength(128)]
        public string StreetName { get; set; }

        [MaxLength(64)]
        public string CityName { get; set; }

        [MaxLength(64)]
        public string CountryName { get; set; }

    }
}
