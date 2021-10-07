using System.ComponentModel.DataAnnotations;

namespace webapp.Models
{
    public class Country
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(64)]
        public string Name { get; set; }
    }
}
