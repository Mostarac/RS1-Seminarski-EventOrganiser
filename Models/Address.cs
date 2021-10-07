using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace webapp.Models
{
    public class Address
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(128)]
        public string Street { get; set; }

        [Required]
        [MaxLength(20)]
        public string Code { get; set; }

        public int CityId { get; set; }

        [ForeignKey("CityId")]
        public City City { get; set; }
        
    }
}
