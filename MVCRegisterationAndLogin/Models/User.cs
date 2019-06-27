using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVCRegisterationAndLogin.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required(AllowEmptyStrings=false , ErrorMessage="First Name required")]
        [MaxLength(50)]
        [Display(Name="First Name")]
        public string FirstName { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Last Name required")]
        [MaxLength(50)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required(AllowEmptyStrings=false , ErrorMessage="Email required")]
        [MaxLength(255)]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Display(Name="Date of birth")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d MMM yyyy}")]
        public DateTime? DateOfBirth { get; set; }

        [Required(AllowEmptyStrings=false ,ErrorMessage="Password required")]
        [DataType(DataType.Password)]
        [MaxLength(255) ,MinLength(6,ErrorMessage="Minimum 6 char required")]
        public string Password { get; set; }

        [Display(Name = "Confirm Password")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Password and confirmation don't match")]
        [NotMapped]
        public string ConfirmPass { get; set; }

        [Required]
        public bool IsEmailVerified { get; set; }

        [Required]
        public System.Guid ActivationCode { get; set; }
    }

}