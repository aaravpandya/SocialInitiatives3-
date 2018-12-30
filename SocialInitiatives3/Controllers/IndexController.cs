using Microsoft.AspNetCore.Mvc;

namespace SocialInitiatives3.Controllers
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