using System.ComponentModel.DataAnnotations;

namespace webapp.Models
{
    public class Role
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
