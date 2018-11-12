using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SocialInitiatives3.Models;
using SocialInitiatives3.Models.ViewModels;

namespace SocialInitiatives3.Controllers
{
    [Route("[controller]/[action]")]
    public class AccountController : Controller
    {
        private UserManager<AppUser> userManager;
        private SignInManager<AppUser> signInManager;
        private AppDbContext AppIdentityDbContext;
        private IMapper _mapper;
        private RoleManager<IdentityRole> _rolmgr;

        public AccountController(UserManager<AppUser> userMgr,
                SignInManager<AppUser> signInMgr, AppDbContext dbContext,IMapper mapper, RoleManager<IdentityRole> roleManager)
        {
            userManager = userMgr;
            signInManager = signInMgr;
            AppIdentityDbContext = dbContext;
            _mapper = mapper;
            _rolmgr = roleManager;
        }

        public IActionResult RegisterV()
        {
            ViewBag.SelectedNav = "Register";
            return View();
        }
       
        public async Task<IActionResult> Register (RegisterModel registerModel)
        {
            //Regex.Match(registerModel.PhoneNumber, @"/[2-9]{2}\d{8}/")
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            AppUser user = null;
            var match = Regex.Match(registerModel.PhoneNumber, @"[2-9]{2}\d{8}");
            var match2 = Regex.Match(registerModel.AdmissionNumber, @"\d{4}");
            if (match.Success && match2.Success)
            {
                user = _mapper.Map<AppUser>(new AppUser(registerModel));
            }
            else
            {
                return BadRequest();
            }
            IdentityResult result = await userManager.CreateAsync(user, registerModel.Password);
            if (!result.Succeeded)
                return new BadRequestObjectResult(result.Errors);

            IdentityResult roleResult;
            var roleCheck = await _rolmgr.RoleExistsAsync("User");
            if (!roleCheck)
            {
                //create the roles and seed them to the database 
                roleResult = await _rolmgr.CreateAsync(new IdentityRole("User"));
            }
            await userManager.AddToRoleAsync(await userManager.FindByEmailAsync(user.Email), "User");
            //await AppIdentityDbContext.AppUsers.AddAsync(user);
            //await AppIdentityDbContext.SaveChangesAsync();
            return Redirect(registerModel?.ReturnUrl ?? "/Index/Home");
        }

        public async Task<IActionResult> Login (LoginModel login)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var user = await userManager.FindByEmailAsync(login.Email);
            if(user != null)
            {
                await signInManager.SignOutAsync();
                if((await signInManager.PasswordSignInAsync(user, login.Password, false, false)).Succeeded)
                {
                    return Redirect(login?.ReturnUrl ?? "/Index/Home");
                }
            }
            ModelState.AddModelError("", "Invalid name or password");
            return BadRequest(ModelState);
        }
        public async Task<IActionResult> LogOut ()
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await signInManager.SignOutAsync();
            return RedirectToAction("Home", "Index");
        }

        public IActionResult ChangePasswordV()
        {
            return View();
        }

        public async Task<IActionResult> ChangePassword(PasswordModel model)
        {
            if(!(model.newPassword == model.confirmPassword))
            {
                ModelState.AddModelError("passwordMismatch", "New password and confirm password do match");
                return BadRequest(ModelState);
            }
            AppUser user = await userManager.GetUserAsync(HttpContext.User);
            IdentityResult result = await userManager.ChangePasswordAsync(user, model.currentPasword, model.newPassword);
            if(result.Succeeded)
                if((await userManager.UpdateAsync(user)).Succeeded)
                    return RedirectToAction("Index", "UserAccount");
            return BadRequest(ModelState);
        }
        public async Task<IActionResult> EditAsync()
        {
            AppUser user = await userManager.GetUserAsync(HttpContext.User);
            AppUserViewModel model = new AppUserViewModel();
            model.Name = user.Name;
            model.PhoneNumber = user.PhoneNumber;
            model.Section = user.Section;
            model._class = user._class;
            model.House = user.House;
            model.Email = user.Email;
            ViewBag.model = model;
            return View();
        }
        public async Task<IActionResult> EditAccount(AppUserViewModel model)
        {
            AppUser user = await userManager.GetUserAsync(HttpContext.User);
            user.Name = model.Name;
            user.Email = model.Email;
            user.PhoneNumber = model.PhoneNumber;
            user._class = model._class;
            user.Section = model.Section;
            user.House = model.House;
            if ((await userManager.UpdateAsync(user)).Succeeded)
                return RedirectToAction("Index", "UserAccount");
            return BadRequest(ModelState);

        }
    }

}