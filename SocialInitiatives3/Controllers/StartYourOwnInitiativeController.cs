using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SocialInitiatives3.Models;
using SocialInitiatives3.Models.ViewModels;

namespace SocialInitiatives3.Controllers
{
    public class StartYourOwnInitiativeController : Controller
    {
        private readonly AppDbContext _dbContext;
        private readonly UserManager<AppUser> _usrmgr;

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
            var s = new SYOI
            {
                cause = vm.cause,
                idea = vm.idea,
                resources = vm.resources,
                targetGroup = vm.targetGroup,
                team = vm.team,
                UserId = _usrmgr.GetUserId(HttpContext.User),
                User = _dbContext.AppUsers.Find(_usrmgr.GetUserId(HttpContext.User)),
                Visible = false
            };
            _dbContext.Add(s);
            _dbContext.SaveChanges();
            return Redirect(vm.returnUrl ?? "/Initiatives/Index");
        }
    }
}