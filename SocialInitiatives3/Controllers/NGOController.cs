using System.Linq;
using Microsoft.AspNetCore.Mvc;
using SocialInitiatives3.Infrastructure;
using SocialInitiatives3.Models;

namespace SocialInitiatives3.Controllers
{
    public class NGOController : Controller
    {
        private readonly AppDbContext _dbContext;

        public NGOController(AppDbContext context)
        {
            _dbContext = context;
        }

        [Route("[controller]")]
        [Route("[controller]/[action]")]
        public IActionResult Index()
        {
            ViewBag.SelectedNav = "NGO";
            return View();
        }

        [Route("[controller]/[action]/{id?}")]
        public IActionResult Categories(string id)
        {
            if (!ModelState.IsValid)
            {
                TempData["Message"] = "Error";
                return Redirect("/Index/Home");
            }

            var success = int.TryParse(id, out var i);
            if (!success)
            {
                TempData["Message"] = "An error was encountered. Please try again.";
                return RedirectToAction("Home", "Index");
            }

            //RegisterModel rm = new RegisterModel();
            //rm.initiatives = _dbContext.initiatives.Where(j => j.categoryId == i);
            ViewBag.SelectedNav = "Initiatives";
            ViewBag.Title = CategoryDict.Categories[i];
            ViewBag.NGOs = _dbContext.nGOs.Where(t => t.categoryId == i).ToList();
            return View();
        }
    }
}