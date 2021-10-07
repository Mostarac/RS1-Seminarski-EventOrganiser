using System.ComponentModel.DataAnnotations;

namespace webapp.ViewModels
{
    public class UserVm
    {
        public string Id { get; set; }

        [Required]
        [Display(Name = "First name")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last name")]
        public string LastName { get; set; }

        public string Email { get; set; }

        public bool EmailConfirmed { get; set; }
        
        public string PasswordHash { get; set; }

        [Required]
        public char Gender { get; set; }

        [Required]
        public string Username { get; set; }

        public string City { get; set; }

        [Required]
        [Display(Name = "City")]
        public int CityId { get; set; }

        public string Role { get; set; }
        
        public string RoleId { get; set; }
    }

    public class RegisterModel
    {
        [Display(Name = "Username")]
        [Required]
        public string UserName { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        [Display(Name = "City")]
        [Required]
        public int CityId { get; set; }

        [Required]
        public char Gender { get; set; }

        [Display(Name = "First name")]
        [Required]
        public string FirstName { get; set; }

        [Display(Name = "Last name")]
        [Required]
        public string LastName { get; set; }

        [Required]
        public string Role { get; set; }

        [Display(Name = "Role")]
        public string RoleId { get; set; }
    }

    public class LoginModel
    {
        [Required]
        [UIHint("email")]
        public string Email { get; set; }

        [Required]
        [UIHint("password")]
        public string Password { get; set; }

    }
}
