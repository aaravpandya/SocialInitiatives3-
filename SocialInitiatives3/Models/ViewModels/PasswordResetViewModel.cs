using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SocialInitiatives3.Models.ViewModels
{
    public class PasswordResetViewModel
    {
        public string email { get; set; }
        public string userId { get; set; }
        public string code { get; set; }
        [UIHint("Password")]
        public string password { get; set; }
        [UIHint("Password")]
        public string Confirmpassword { get; set; }
    }
}
