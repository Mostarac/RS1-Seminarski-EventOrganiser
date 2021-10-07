using System.ComponentModel.DataAnnotations;

namespace webapp.Models
{
    public class VenueType
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(64)]
        public string Name { get; set; }
    }
}
