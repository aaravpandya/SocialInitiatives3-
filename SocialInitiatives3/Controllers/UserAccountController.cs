using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SocialInitiatives3.Models;
using Microsoft.EntityFrameworkCore;
using SocialInitiatives3.Models.ViewModels;

namespace SocialInitiatives3.Controllers
{
    public class UserAccountController : Controller
    {
        private AppDbContext _dbContext;
        private UserManager<AppUser> _usrmgr;

        public UserAccountController(AppDbContext appDbContext, UserManager<AppUser> userManager)
        {
            _dbContext = appDbContext;
            _usrmgr = userManager;
        }
        [Authorize]
        [Route("[controller]")]
        [Route("[controller]/[action]")]
        public async Task<IActionResult> IndexAsync()
        {
            AppUser i = _dbContext.AppUsers.Include(user => user.user_initiatives_created).Include(u => u.UserVolunteers).Include(u => u.user_events).SingleOrDefault(user => user.Id == _usrmgr.GetUserId(HttpContext.User));
            List<Initiative> initiatives_user_created = i.user_initiatives_created;
            List<AppUserViewModel> users = new List<AppUserViewModel>();

            List<Initiative> user_has_volunteered = new List<Initiative>();
            foreach (UserVolunteer uv in i.UserVolunteers)
            {
                Initiative init = _dbContext.initiatives.Include(u => u.User).SingleOrDefault(u => u.InitiativeId == uv.initiativeId);
                if(init!=null)
                user_has_volunteered.Add(init);

            }
            foreach (Initiative init in initiatives_user_created)
            {
                Initiative initiative = _dbContext.initiatives.Include(u => u.UserVolunteers).SingleOrDefault(u => u.InitiativeId == init.InitiativeId);

                foreach (UserVolunteer uv in initiative.UserVolunteers)
                {
                    AppUser user = await _usrmgr.FindByIdAsync(uv.userId);
                    users.Add(new AppUserViewModel() { Name = user.Name, AdmissionNumber = user.AdmissionNumber, Email = user.Email, House = user.House, PhoneNumber = user.PhoneNumber, Section = user.Section, _class = user._class });
                }
            }
            List<Event> events_user_created = i.user_events;
            //var t = i.Select(item => item.user_initiatives_created).ToList();
            //List<AppUserViewModel> uvmodels = new List<AppUserViewModel>();
            //List<Initiative> inits = new List<Initiative>();
            //foreach(var a in t)
            //{
            //    foreach(Initiative init in a)
            //    {
            //        Initiative x = _dbContext.initiatives.Include(u => u.UserVolunteers).SingleOrDefault(u => u.InitiativeId == init.InitiativeId);
            //        inits.Add(x);
            //        foreach(UserVolunteer uv in x.UserVolunteers)
            //        {
            //            uvmodels.Add(new AppUserViewModel(){Name =uv.user.Name, AdmissionNumber=uv.user.AdmissionNumber, Email=uv.user.Email, House=uv.user.House, PhoneNumber = uv.user.PhoneNumber, Section=uv.user.Section, _class=uv.user._class});
            //        }
            //    }
            //}
            //List<Initiative> uvli = new List<Initiative>();
            //foreach (var z in i)
            //{
                
            //    foreach (var y in z.UserVolunteers)
            //    {
            //        uvli.Add(y.initiative);
            //    }
            //}
            //var e = i.Select(item => item.user_events).ToList();
            //List<Event> events = new List<Event>();
            //foreach (var z in e)
            //{
            //    foreach(Event a in z)
            //    {
            //        events.Add(a);
            //    }
            //}
            ViewBag.Initiatives = initiatives_user_created;
            ViewBag.uvlist = user_has_volunteered;
            ViewBag.Events = events_user_created;
            
            ViewBag.UserVolunteers = users.GroupBy(x => x.Email).Select(x => x.First()).ToList();
            return View();
        }

        [Authorize(Roles = "Admin")]
        [Route("[controller]/[action]/{e?}")]
        public async Task<IActionResult> UserInfoAsync(string e)
        {
            AppUser user = await _usrmgr.FindByEmailAsync(e);
            if (user == null)
                return BadRequest(ModelState);
            List<AppUser> i = _dbContext.AppUsers.Where(u => u.Email == user.Email).Include(u => u.user_initiatives_created).Include(u => u.UserVolunteers).Include(u => u.user_events).ToList();
            var t = i.Select(item => item.user_initiatives_created).ToList();
            List<Initiative> uvli = new List<Initiative>();
            foreach (var z in i)
            {

                foreach (var y in z.UserVolunteers)
                {
                    uvli.Add(y.initiative);
                }
            }
            var es = i.Select(item => item.user_events).ToList();
            List<Event> events = new List<Event>();
            foreach (var z in es)
            {
                foreach (Event a in z)
                {
                    events.Add(a);
                }
            }
            AppUserViewModel userModel =  new AppUserViewModel() { Name = user.Name, AdmissionNumber = user.AdmissionNumber, Email = user.Email, House = user.House, PhoneNumber = user.PhoneNumber, Section = user.Section, _class = user._class };
            ViewBag.Initiatives = t;
            ViewBag.uvlist = uvli;
            ViewBag.events = events;
            ViewBag.userModel = userModel;
            return View();

        }
    }
}