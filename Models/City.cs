using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace webapp.Models
{
    public class City
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(64)]
        public string Name { get; set; }

        public int CountryId { get; set; }

        [ForeignKey("CountryId")]
        public Country Country { get; set; }

    }
}
