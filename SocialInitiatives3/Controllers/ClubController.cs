using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SocialInitiatives3.Models;
using SocialInitiatives3.Models.ViewModels;

namespace SocialInitiatives3.Controllers
{
    [Route("[controller]/[action]")]
    
    public class ClubController : Controller
    {
        private AppDbContext _DbContext;

        public ClubController(AppDbContext appDbContext)
        {
            _DbContext = appDbContext;
        }

        [Route("[controller]")]
        public IActionResult Index()
        {
            ViewBag.selectedNav = "Club";
            return View();
        }

        [Route("[controller]/[action]")]
        public IActionResult PostForm (ClubViewModel clubViewModel)
        {
            ClubUser cu = new ClubUser
            {
                UserName = clubViewModel.name,
                Class = clubViewModel.Class,
                Section = clubViewModel.Section
            };
            _DbContext.clubUsers.Add(cu);
            _DbContext.SaveChanges();
            return Redirect("/Club");
        }


    }
}