using System.Linq;
using Microsoft.AspNetCore.Mvc;
using SocialInitiatives3.Models;

namespace SocialInitiatives3.Controllers
{
    public class ApiController : Controller
    {
        private readonly AppDbContext dc;

        public ApiController(AppDbContext appADbContext)
        {
            dc = appADbContext;
        }

        [Route("[controller]/[action]")]
        public JsonResult GetEvents()
        {
            var events = dc.events.Where(j => j.Visible).ToList();
            return Json(events);
        }
    }
}