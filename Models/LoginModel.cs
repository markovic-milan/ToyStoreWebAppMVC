using System.ComponentModel.DataAnnotations;

namespace ToyStoreWebAppMVC.Models
{
    public class LoginModel
    {
        [Required]
        [StringLength(100, MinimumLength = 2)]
        public string Username { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 2)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public string LoginInValid { get; set; }
    }
}
