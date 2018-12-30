using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace SocialInitiatives3.Models
{
    public class Event
    {
        public int EventID { get; set; }
        public string Subject { get; set; }
        public string Organiser { get; set; }
        public string PhoneNumber { get; set; }
        public string OrganiserEmail { get; set; }
        public string Description { get; set; }
        public DateTime Start { get; set; }
        public DateTime? End { get; set; }
        public string ThemeColor { get; set; }
        public bool IsFullDay { get; set; }

        public string UserId { get; set; }

        [ForeignKey("UserId")] public AppUser User { get; set; }

        public bool Visible { get; set; }
    }
}