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
    public class CalendarController : Controller
    {
        private AppDbContext dc;
        private UserManager<AppUser> _usrmgr;

        public CalendarController(AppDbContext appADbContext, UserManager<AppUser> userManager)
        {
            dc = appADbContext;
            _usrmgr = userManager;
        }

        [Route("[controller]/[action]")]
        public IActionResult Index()
        {
            ViewBag.SelectedNav = "Calendar";
            return View();
        }

        [Authorize]
        [Route("[controller]/[action]")]
        public IActionResult AddEvents (EventViewModel viewModel)
        {
            if(!ModelState.IsValid)
            {
                TempData["Message"] = "Error";
                return Redirect("/Index/Home");
            }
            Event e = new Event();
            e.Subject = viewModel.Subject;
            e.Organiser = viewModel.Organizer;
            e.Start = DateTime.ParseExact( viewModel.Start, "MM/dd/yyyy h:mm tt", System.Globalization.CultureInfo.InvariantCulture);
            if(viewModel.End != null)
            {
                e.End = e.Start = DateTime.ParseExact(viewModel.End, "MM/dd/yyyy h:mm tt", System.Globalization.CultureInfo.InvariantCulture);
            }
            else
            {
                e.End = e.Start;
            }
            e.Description = "<div>" + viewModel.Description + " <br>Organizer : " + viewModel.Organizer + "<br>OrganizerEmail : " + viewModel.OrganizerEmail+ "<br>Organizer Phone Number : "+ viewModel.PhoneNumber + "<br>Start time and date : " + e.Start + "<br></div > ";
            e.OrganiserEmail = viewModel.OrganizerEmail;
            e.PhoneNumber = viewModel.PhoneNumber;
            e.ThemeColor = "Green";
            e.IsFullDay = true;
            e.UserId = _usrmgr.GetUserId(HttpContext.User);
            e.User = dc.AppUsers.Find(_usrmgr.GetUserId(HttpContext.User));
            e.Visible = false;
            dc.Add(e);
            dc.SaveChanges();
            return Redirect(viewModel?.returnUrl ?? "/Calendar/Index");
        }
    }
}