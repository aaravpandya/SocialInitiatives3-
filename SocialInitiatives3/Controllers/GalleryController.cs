using Microsoft.AspNetCore.Mvc;

namespace SocialInitiatives3.Controllers
{
    public class GalleryController : Controller
    {
        [Route("[controller]/[action]")]
        public IActionResult Index()
        {
            ViewBag.SelectedNav = "Gallery";
            return View();
        }
    }
}