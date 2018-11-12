using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialInitiatives3.Models
{
    public class UserVolunteer
    {
        public string userId { get; set; }
        public AppUser user { get; set; }

        public int initiativeId { get; set; }
        public Initiative initiative { get; set; }
    }
}
