using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SocialInitiatives3.Models;

namespace SocialInitiatives3.Controllers
{
    public class ApiController : Controller
    {
        private AppDbContext dc;

        public ApiController(AppDbContext appADbContext)
        {
            dc = appADbContext;
        }

        [Route("[controller]/[action]")]
        public JsonResult GetEvents()
        {
            var events = dc.events.Where(j => j.Visible==true).ToList();
            return Json(events);

        }
    }
}