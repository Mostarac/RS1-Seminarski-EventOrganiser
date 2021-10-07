using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace webapp.Models
{
    public class AppUser : IdentityUser
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }   

        public char Gender { get; set; }

        public int CityId { get; set; }

        [ForeignKey("CityId")]
        public City Cities { get; set; }
    }
}
