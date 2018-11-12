using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SocialInitiatives3.Models;
using Microsoft.EntityFrameworkCore;
using SocialInitiatives3.Models.ViewModels;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace SocialInitiatives3.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private AppDbContext _dbContext;
        private IHostingEnvironment he;

        public AdminController(AppDbContext dbContext, IHostingEnvironment hostingEnvironment)
        {
            _dbContext = dbContext;
            he = hostingEnvironment;
        }

        [Route("[controller]")]
        [Route("[controller]/[action]")]
        public IActionResult Index()
        {
            var i = _dbContext.initiatives.Include(initiative => initiative.User).Where(j => j.Visible == false);
            ViewBag.initiatives = i;
            ViewBag.SelectedNav = "Initiatives";
            return View();
        }

        [Route("[controller]/[action]/{id?}/{init?}")]
        public IActionResult InitiativeButton(int id, int init)
        {
            Initiative i = _dbContext.initiatives.Find(init);
            if(id == 1)
            {
                i.Visible = true;
                _dbContext.Update(i);
                _dbContext.SaveChanges();
                return RedirectToAction("Index", "Admin");
            }
            else if(id == 2)
            {
                _dbContext.initiatives.Remove(i);
                _dbContext.SaveChanges();
                return RedirectToAction("Index", "Admin");
            }
            return RedirectToAction("Index", "Admin");
        }

        [Route("[controller]/[action]")]
        public IActionResult Events()
        {
            List<Event> events = _dbContext.events.Include(j => j.User).Where(k => k.Visible == false).ToList();
            ViewBag.events = events;
            ViewBag.SelectedNav = "Events";
            return View();
        }

        [Route("[controller]/[action]/{id?}/{init?}")]
        public IActionResult EventButton(int id, int init)
        {
            Event e = _dbContext.events.Find(init);
            if (id == 1)
            {
                e.Visible = true;
                _dbContext.Update(e);
                _dbContext.SaveChanges();
                return RedirectToAction("Events", "Admin");
            }
            else if (id == 2)
            {
                _dbContext.events.Remove(e);
                _dbContext.SaveChanges();
                return RedirectToAction("Events", "Admin");
            }
            return RedirectToAction("Events", "Admin");
        }

        [Route("[controller]/[action]")]
        public IActionResult SYOI()
        {
            ViewBag.SYOIs = _dbContext.ownInitiatives.Include(j => j.User).Where(k => k.Visible == false).ToList();
            ViewBag.SelectedNav = "SYOI";
            return View();
        }
        [Route("[controller]/[action]/{init?}")]
        public IActionResult SYOIDeleteButton(int init)
        {
            SYOI s = _dbContext.ownInitiatives.Find(init);
            _dbContext.Remove(s);
            _dbContext.SaveChanges();
            return RedirectToAction("SYOI", "Admin");
        }

        [Route("[controller]/[action]")]
        public IActionResult UserList()
        {
            List<AppUserViewModel> appUsers = new List<AppUserViewModel>();
            List<AppUser> list = _dbContext.AppUsers.ToList();
            foreach(AppUser u in list)
            {
                appUsers.Add(new AppUserViewModel() { Name =u.Name, AdmissionNumber=u.AdmissionNumber, Email=u.Email, House=u.House, PhoneNumber = u.PhoneNumber, Section=u.Section, _class=u._class });
            }
            ViewBag.SelectedNav = "UserList";
            ViewBag.list = appUsers;
            return View();
        }

        [Route("[controller]/[action]")]
        public IActionResult ClubForms()
        {
            List<ClubUser> clubUsers = _dbContext.clubUsers.ToList();
            ViewBag.SelectedNav = "ClubForms";
            ViewBag.clubUsers = clubUsers;
            return View();
        }

        [Route("[controller]/[action]")]
        public IActionResult AddNGO(NgoViewModel viewModel)
        {
            NGO ngo = new NGO
            {
                NgoName = viewModel.NgoName,
                NgoAddress = viewModel.NgoAddress
            };
            ngo.NGOId = 0;
            ngo.work = viewModel.work;
            ngo.team = viewModel.team;
            ngo.facebookLink = viewModel.facebookLink;
            ngo.instagramLink = viewModel.instagramLink;
            ngo.phoneNumber = viewModel.phoneNumber;
            ngo.websiteLink = viewModel.websiteLink;
            IFormFile uploadedImage = viewModel.imageUpload;

            if ((uploadedImage != null) || uploadedImage.ContentType.ToLower().StartsWith("Image/"))
            {
                //    var root = he.WebRootPath;
                //    root = root + "\\SubmittedInitiativeImg";
                ////same file name problems
                //var filename = Path.Combine(he.WebRootPath, Path.GetFileName(uploadedImage.FileName));
                var name = Guid.NewGuid().ToString() + Path.GetFileName(uploadedImage.FileName);
                var filename = Path.Combine(he.WebRootPath, name);
                
                uploadedImage.CopyTo(new FileStream(filename, FileMode.Create));
                ngo.filepath = "/" + name;
            }
            ngo.categoryId = viewModel.categoryId;
            ngo.Category = viewModel.Category;
            _dbContext.Add(ngo);
            _dbContext.SaveChanges();
            return Redirect(viewModel?.returnUrl ?? "/Admin/NGOS");
        }

        [Route("[controller]/[action]")]
        public IActionResult NGOS ()
        {
            var n = _dbContext.nGOs.ToList();
            ViewBag.ngos = n;
            return View();
        }

        [Route("[controller]/[action]/{id?}")]
        public IActionResult NgoButton(int id)
        {
            var n = _dbContext.nGOs.Find(id);
            _dbContext.Remove(n);
            _dbContext.SaveChanges();
            return Redirect("/Admin/NGOS");
        }
            
    }
}
