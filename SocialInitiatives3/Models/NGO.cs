using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SocialInitiatives3.Models
{
    public class NGO
    {
        public int NGOId { get; set; }

        [Required]
        public string NgoName { get; set; }

        [Required]
        public string work { get; set; }
        public string team { get; set; }
        public string phoneNumber { get; set; }
        public string websiteLink { get; set; }
        public string facebookLink { get; set; }
        public string instagramLink { get; set; }

        [Required]
        public string NgoAddress { get; set; }
        //[Required]
        //public Image Image { get; set; }
        [Required]
        public string filepath { get; set; }
        public int categoryId { get; set; }
        public string Category { get; set; }
        
    }
}
