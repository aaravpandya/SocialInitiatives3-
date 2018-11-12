using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SocialInitiatives3.Models.ViewModels
{
    public class PasswordModel
    {
        [Required]
        [UIHint("password")]
        public string newPassword { get; set; }
        [Required]
        [UIHint("password")]
        public string confirmPassword { get; set; }
        [Required]
        [UIHint("password")]
        public string currentPasword { get; set; }
    }
}
