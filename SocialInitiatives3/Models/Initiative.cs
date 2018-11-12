using SocialInitiatives3.Infrastructure;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SocialInitiatives3.Models
{
    public class Initiative
    {
        public int InitiativeId { get; set; }

        [Required]
        public string InitiativeName { get; set; }

        [Required]
        public string work { get; set; }
        public string team { get; set; }
        public string phoneNumber { get; set; }
        public string websiteLink { get; set; }
        public string facebookLink { get; set; }
        public string instagramLink { get; set; }

        [Required]
        public string InitiativeAddress { get; set; }
        //[Required]
        //public Image Image { get; set; }
        [Required]
        public string filepath { get; set; }
        public int categoryId { get; set; }
        public string Category { get; set; }

        public virtual List<UserVolunteer> UserVolunteers { get; set; }
        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public AppUser User { get; set; }
        public bool Visible { get; set; }
    }
}
