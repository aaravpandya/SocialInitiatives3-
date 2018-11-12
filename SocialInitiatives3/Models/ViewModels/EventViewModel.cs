using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SocialInitiatives3.Models.ViewModels
{
    public class EventViewModel
    {
        [Required]
        public string returnUrl { get; set; }
        [Required]
        public string Subject { get; set; }
        [Required]
        public string Organizer { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        [Required]
        public string OrganizerEmail { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string Start { get; set; }
        public string End { get; set; }
    }
}
