using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SocialInitiatives3.Models;
using SocialInitiatives3.Models.ViewModels;

namespace SocialInitiatives3.Controllers
{
    public class StartYourOwnInitiativeController : Controller
    {
        private AppDbContext _dbContext;
        private UserManager<AppUser> _usrmgr;

        public StartYourOwnInitiativeController(AppDbContext appADbContext, UserManager<AppUser> userManagers)
        {
            _dbContext = appADbContext;
            _usrmgr = userManagers;
        }

        [Route("[controller]/[action]")]
        public IActionResult Index()
        {
            ViewBag.SelectedNav = "SYOI";
            return View();
        }
        [Authorize]
        [Route("[controller]/[action]")]
        public IActionResult PublishForm(SYOIViewModel vm)
        {
            SYOI s = new SYOI();
            s.cause = vm.cause;
            s.idea = vm.idea;
            s.resources = vm.resources;
            s.targetGroup = vm.targetGroup;
            s.team = vm.team;
            s.UserId = _usrmgr.GetUserId(HttpContext.User);
            s.User = _dbContext.AppUsers.Find(_usrmgr.GetUserId(HttpContext.User));
            s.Visible = false;
            _dbContext.Add(s);
            _dbContext.SaveChanges();
            return Redirect(vm?.returnUrl ?? "/Initiatives/Index");
        }
    }
}