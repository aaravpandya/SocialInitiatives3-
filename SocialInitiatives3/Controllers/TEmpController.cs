using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace SocialInitiatives3.Controllers
{
    [Route("Temp")]
    public class TEmpController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}