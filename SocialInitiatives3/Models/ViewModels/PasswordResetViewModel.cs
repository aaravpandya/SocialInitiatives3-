namespace SocialInitiatives3.Models.ViewModels
{
    public class PasswordResetViewModel
    {
        public string email { get; set; }
        public string userId { get; set; }
        public string code { get; set; }

        public string password { get; set; }

        public string Confirmpassword { get; set; }
    }
}