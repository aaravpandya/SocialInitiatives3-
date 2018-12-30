using System.ComponentModel.DataAnnotations;

namespace SocialInitiatives3.Models.ViewModels
{
    public class PasswordModel
    {
        [Required] public string newPassword { get; set; }

        [Required] public string confirmPassword { get; set; }

        [Required] public string currentPasword { get; set; }
    }
}