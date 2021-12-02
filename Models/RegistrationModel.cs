using System.ComponentModel.DataAnnotations;

namespace ToyStoreWebAppMVC.Models
{
    public class RegistrationModel
    {
        [Required]
        [StringLength(20, MinimumLength = 4)]
        [Display(Name = "User Name")]
        public string Username { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [Display(Name = "First Name")]
        [StringLength(100, MinimumLength = 4)]
        public string FirstName { get; set; }
        [Required]
        [Display(Name = "Last Name")]
        [StringLength(100, MinimumLength = 4)]
        public string LastName { get; set; }
        public string RegistrationInValid { get; set; }
    }
}
