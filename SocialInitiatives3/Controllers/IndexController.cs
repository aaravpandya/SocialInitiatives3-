using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Social_Initiatives.Controllers
{

    [Route("")]
    [Route("[controller]/[action]")]
    public class IndexController : Controller
    {

        public IActionResult Home()
        {
            ViewBag.SelectedNav = "Home";
            return View();
        }
    }
}