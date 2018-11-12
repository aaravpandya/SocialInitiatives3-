using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SocialInitiatives3.Models
{
    public class SYOI
    {
        public int SYOIId { get; set; }

        public string cause { get; set; }
        public string targetGroup { get; set; }
        public string team { get; set; }
        public string idea { get; set; }
        public string resources { get; set; }


        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public AppUser User { get; set; }

        public bool Visible { get; set; }
    }
}
