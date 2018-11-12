using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace SocialInitiatives3.Controllers
{
    [Route("[controller]/[action]")]
    public class ContactUsController : Controller
    {
        public IActionResult Index()
        {
            ViewBag.SelectedNav = "ContactUs";
            return View();
        }
    }
}