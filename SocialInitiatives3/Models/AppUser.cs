using Microsoft.AspNetCore.Identity;
using SocialInitiatives3.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SocialInitiatives3.Models
{
    public class AppUser : IdentityUser
    {
        public AppUser(RegisterModel registerModel) : base()
        {
            UserName = Regex.Replace(registerModel.Name, @"\s+", "");
            Name = registerModel.Name;
            Email = registerModel.Email;
            PhoneNumber = registerModel.PhoneNumber;
            AdmissionNumber = registerModel.AdmissionNumber;
            _class = registerModel._class;
            Section = registerModel.Section;
            House = registerModel.House;
            
        }
        public AppUser() : base() { }
        public string Name { get; set; }
        public string AdmissionNumber { get; set; }
        public string _class { get; set; }
        public string Section { get; set; }
        public string House { get; set; }
        public virtual List<Initiative> user_initiatives_created { get; set; }
        public virtual List<Event> user_events { get; set; }
        public virtual List<UserVolunteer> UserVolunteers { get; set; }
        public virtual List<SYOI> sYOIs_user_created { get; set; }
        public bool club_signed_up { get; set; }
    }
}
