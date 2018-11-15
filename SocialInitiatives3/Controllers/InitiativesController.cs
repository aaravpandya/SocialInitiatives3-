using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SocialInitiatives3.Models;
using SocialInitiatives3.Models.ViewModels;
using System.Drawing;
using Microsoft.EntityFrameworkCore.Extensions.Internal;
using SocialInitiatives3.Infrastructure;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace SocialInitiatives3.Controllers
{
    public class InitiativesController : Controller
    {
        public AppDbContext _dbContext;
        private IHostingEnvironment he;
        private UserManager<AppUser> _usermgr;

        public InitiativesController(AppDbContext dbContext, IHostingEnvironment hostingEnvironment, UserManager<AppUser> userManager)
        {
            _dbContext = dbContext;
            he = hostingEnvironment;
            _usermgr = userManager;
        }

        [Route("Initiatives")]
        [Route("[controller]/[action]")]
        public IActionResult Index()
        {
            ViewBag.SelectedNav = "Initiatives";
            return View();
        }

        [Route("[controller]/[action]")]
        [Authorize]
        public IActionResult PostInitiativeForm(InitiativeModel initiativeModel)
        {
            Initiative initiative = new Initiative
            {
                InitiativeName = initiativeModel.Name,
                InitiativeAddress = initiativeModel.InitiativeAddress
            };
            initiative.InitiativeId = 0;
            initiative.work = initiativeModel.work;
            initiative.team = initiativeModel.team;
            initiative.facebookLink = initiativeModel.facebookLink;
            initiative.instagramLink = initiativeModel.instagramLink;
            initiative.phoneNumber = initiativeModel.phoneNumber;
            initiative.websiteLink = initiativeModel.websiteLink;
            IFormFile uploadedImage = initiativeModel.imageUpload;

            if ((uploadedImage != null) || uploadedImage.ContentType.ToLower().StartsWith("Image/"))
            {
                //    var root = he.WebRootPath;
                //    root = root + "\\SubmittedInitiativeImg";
                ////same file name problems
                //var filename = Path.Combine(he.WebRootPath, Path.GetFileName(uploadedImage.FileName));
                var name = Guid.NewGuid().ToString() + Path.GetFileName(uploadedImage.FileName);
                var filename = Path.Combine(he.WebRootPath, name);

                uploadedImage.CopyTo(new FileStream(filename, FileMode.Create));
                initiative.filepath = "/" + name;
            }
            initiative.UserId = _usermgr.GetUserId(HttpContext.User);
            initiative.User = _dbContext.AppUsers.Find(_usermgr.GetUserId(HttpContext.User));

            //{
            //    MemoryStream ms = new MemoryStream();
            //    uploadedImage.OpenReadStream().CopyTo(ms);
            //    System.Drawing.Image image = System.Drawing.Image.FromStream(ms);
            //    Infrastructure.Image imageEntity = new Infrastructure.Image();
            //    imageEntity.Name = uploadedImage.Name;
            //    imageEntity.Data = ms.ToArray();
            //    imageEntity.Width = image.Width;
            //    imageEntity.Height = image.Height;
            //    imageEntity.ContentType = uploadedImage.ContentType;
            //    initiative.Image = imageEntity;

            //}

            initiative.categoryId = initiativeModel.categoryId;
            initiative.Category = CategoryDict.Categories[initiative.categoryId];
            initiative.Visible = false;
            _dbContext.Add(initiative);
            _dbContext.SaveChanges();
            return Redirect(initiativeModel?.returnUrl ?? "/Initiatives/Index");
        }

        [Route("[controller]/[action]/{id?}")]
        public IActionResult Category(string id)
        {
            if (!ModelState.IsValid)
            {
                TempData["Message"] = "An error was encountered. Please try again.";
                return RedirectToAction("Home", "Index");
            }
            int i = 0;
            var success = Int32.TryParse(id, out i);
            if (!success)
            {
                TempData["Message"] = "An error was encountered. Please try again.";
                return RedirectToAction("Home", "Index");
            }
            ViewBag.SelectedNav = "Initiatives";
            ViewBag.Title = CategoryDict.Categories[i];
            ViewBag.Color = CategoryDict.CatColor[i];
            ViewBag.initiatives = _dbContext.initiatives.Include(initiative => initiative.User).Where(j => j.categoryId == i && j.Visible == true).ToList();
            List<AppUser> users = _dbContext.AppUsers.Include(user => user.user_initiatives_created).Include(u => u.UserVolunteers).Where(user => user.Id == _usermgr.GetUserId(HttpContext.User)).ToList();
            List<Initiative> uvli = new List<Initiative>();
            
            foreach (var z in users)
            {
                foreach (var y in z.UserVolunteers)
                {
                    uvli.Add(y.initiative);
                }
            }
            ViewBag.uvlist = uvli;
            ViewBag.Page = "Category";
            return View();
        }
        [Authorize]
        [Route("[controller]/[action]/{id?}")]
        public IActionResult Volunteer(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            AppUser user = _dbContext.AppUsers.Include(j => j.UserVolunteers).SingleOrDefault(j => j.Id == _usermgr.GetUserId(HttpContext.User));
            Initiative i = _dbContext.initiatives.Include(k => k.UserVolunteers).SingleOrDefault(k => k.InitiativeId == id);
            UserVolunteer uv = new UserVolunteer();
            uv.initiative = i;
            uv.initiativeId = i.InitiativeId;
            uv.user = user;
            uv.userId = user.Id;
            user.UserVolunteers.Add(uv);
            i.UserVolunteers.Add(uv);
            _dbContext.Update(user);
            _dbContext.Update(i);
            _dbContext.SaveChanges();
            return RedirectToAction("Category", "Initiatives", i.categoryId);

        }

        //[Route("[controller]/[action]/{id?}")]
        //public FileStreamResult GetFile(string id)
        //{
        //    Int32.TryParse(id, out int i);
        //    Infrastructure.Image image = _dbContext.initiatives.Find(i).Image;
        //    Stream stream = new MemoryStream(image.Data);
        //    return new FileStreamResult(stream, image.ContentType);
        //}


    }
}
