using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SocialInitiatives3.Models.ViewModels
{
    public class InitiativeModel
    {
        [Required]
        public string returnUrl { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string team { get; set; }

        [Required]
        public string work { get; set; }

        [Required]
        public string InitiativeAddress { get; set; }

        [Required]
        public IFormFile imageUpload { get; set; } 

        [Required]
        public int categoryId { get; set; }

        //public string email { get; set; }
        public string phoneNumber { get; set; }
        public string websiteLink { get; set; }
        public string facebookLink { get; set; }
        public string instagramLink { get; set; }
    }
}
