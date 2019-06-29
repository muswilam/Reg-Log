using System.ComponentModel.DataAnnotations;

namespace MVCRegisterationAndLogin.Models
{
    public class UserLogin
    {
        [Required(AllowEmptyStrings=false , ErrorMessage="Email required")]
        [MaxLength(255)]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(AllowEmptyStrings=false ,ErrorMessage="Password required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name="Remember Me")]
        public bool RememberMe { get; set; }
    }
}