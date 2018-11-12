using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialInitiatives3.Infrastructure
{
    public static class CategoryDict
    {
        public static Dictionary<int, string> Categories = new Dictionary<int, string>()
        {
            {1, "Poverty and Unemployment "},
            {2, "Healthcare and Sanitation" },
            {3, "Quality Education" },
            {4,  "Gender Equality"},
            {5, "Climate Action and Energy"},
            {6, "Rural Development"},
            {7, "Uplifting the Minorities"},
            {8, "Veterinarian Initiatives"},
            {9, "Basic Skills Development"}
        };
        public static Dictionary<int,string> CatColor = new Dictionary<int, string>()
        {
            {1, @"#006b5b"},
            {2, @"#c64c37" },
            {3, @"#0f167e" },
            {4, @"#fe0058"},
            {5, @"#9bafd4"},
            {6, @"#b68250"},
            {7, @"#ffa269"},
            {8, @"#f0c419"},
            {9, @"#9f6363"}
        };
    }
}
