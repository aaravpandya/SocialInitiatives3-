using System.ComponentModel.DataAnnotations;

namespace SocialInitiatives3.Models.ViewModels
{
    public class RegisterModel
    {
        [Required(ErrorMessage = "Please Enter your email")]
        [EmailAddress]
        public string Email { get; set; }

        [Required] public string Name { get; set; }

        [Required] public string Password { get; set; }

        [Required] public string PhoneNumber { get; set; }

        [Required] public string ReturnUrl { get; set; } = "/";

        [Required] public string AdmissionNumber { get; set; }

        [Required] public string _class { get; set; }

        [Required] public string Section { get; set; }

        [Required] public string House { get; set; }
    }
}