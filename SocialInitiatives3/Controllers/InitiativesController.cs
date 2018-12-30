using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SocialInitiatives3.Infrastructure;
using SocialInitiatives3.Models;
using SocialInitiatives3.Models.ViewModels;

namespace SocialInitiatives3.Controllers
{
    public class InitiativesController : Controller
    {
        private readonly AppDbContext _dbContext;
        private readonly UserManager<AppUser> _usermgr;
        private readonly IHostingEnvironment he;

        public InitiativesController(AppDbContext dbContext, IHostingEnvironment hostingEnvironment,
            UserManager<AppUser> userManager)
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
            var initiative = new Initiative
            {
                InitiativeName = initiativeModel.Name,
                InitiativeAddress = initiativeModel.InitiativeAddress,
                InitiativeId = 0,
                work = initiativeModel.work,
                team = initiativeModel.team,
                facebookLink = initiativeModel.facebookLink,
                instagramLink = initiativeModel.instagramLink,
                phoneNumber = initiativeModel.phoneNumber,
                websiteLink = initiativeModel.websiteLink
            };
            var uploadedImage = initiativeModel.imageUpload;

            if (uploadedImage != null && uploadedImage.ContentType.ToLower().StartsWith("image/"))
            {
                //    var root = he.WebRootPath;
                //    root = root + "\\SubmittedInitiativeImg";
                ////same file name problems
                //var filename = Path.Combine(he.WebRootPath, Path.GetFileName(uploadedImage.FileName));
                var name = Guid.NewGuid() + Path.GetFileName(uploadedImage.FileName);
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
            return Redirect(initiativeModel.returnUrl ?? "/Initiatives/Index");
        }

        [Route("[controller]/[action]/{id?}")]
        public IActionResult Category(string id)
        {
            if (!ModelState.IsValid)
            {
                TempData["Message"] = "An error was encountered. Please try again.";
                return RedirectToAction("Home", "Index");
            }

            var success = int.TryParse(id, out var i);
            if (!success)
            {
                TempData["Message"] = "An error was encountered. Please try again.";
                return RedirectToAction("Home", "Index");
            }

            ViewBag.SelectedNav = "Initiatives";
            ViewBag.Title = CategoryDict.Categories[i];
            ViewBag.Color = CategoryDict.CatColor[i];
            ViewBag.initiatives = _dbContext.initiatives.Include(initiative => initiative.User)
                .Where(j => j.categoryId == i && j.Visible).ToList();
            var users = _dbContext.AppUsers.Include(user => user.user_initiatives_created)
                .Include(u => u.UserVolunteers).Where(user => user.Id == _usermgr.GetUserId(HttpContext.User)).ToList();
            var uvli = new List<Initiative>();

            foreach (var z in users)
            foreach (var y in z.UserVolunteers)
                uvli.Add(y.initiative);
            ViewBag.uvlist = uvli;
            ViewBag.Page = "Category";
            return View();
        }

        [Authorize]
        [Route("[controller]/[action]/{id?}")]
        public IActionResult Volunteer(int id)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var user = _dbContext.AppUsers.Include(j => j.UserVolunteers)
                .SingleOrDefault(j => j.Id == _usermgr.GetUserId(HttpContext.User));
            var i = _dbContext.initiatives.Include(k => k.UserVolunteers).SingleOrDefault(k => k.InitiativeId == id);
            if (i != null)
            {
                var uv = new UserVolunteer
                    {initiative = i, initiativeId = i.InitiativeId, user = user, userId = user?.Id};
                user?.UserVolunteers.Add(uv);
                i.UserVolunteers.Add(uv);
            }

            _dbContext.Update(user ?? throw new InvalidOperationException());
            _dbContext.Update(i ?? throw new InvalidOperationException());
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