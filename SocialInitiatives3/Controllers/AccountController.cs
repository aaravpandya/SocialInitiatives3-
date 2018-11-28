using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Encodings.Web;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SendGrid;
using SendGrid.Helpers.Mail;
using SocialInitiatives3.Infrastructure;
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

        public async Task<IActionResult> Register(RegisterModel registerModel)
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
                TempData["Message"] = "Invalid phone number or admission number. Please try again with correct details.";
                return RedirectToAction("Home", "Index");
            }
            IdentityResult result = await userManager.CreateAsync(user, registerModel.Password);
            if (!result.Succeeded)
            { 
                TempData["Message"] = "Error in creating user. Please try again.";
                return RedirectToAction("Home", "Index");
            }

            IdentityResult roleResult;
            var roleCheck = await _rolmgr.RoleExistsAsync("User");
            if (!roleCheck)
            {
                //create the roles and seed them to the database 
                roleResult = await _rolmgr.CreateAsync(new IdentityRole("User"));
            }
            user = await userManager.FindByEmailAsync(user.Email);
            roleCheck = await _rolmgr.RoleExistsAsync("Admin");
            if (!roleCheck)
            {
                //create the roles and seed them to the database 
                await _rolmgr.CreateAsync(new IdentityRole("Admin"));
            }
            //Assign Admin role to the main User here we have given our newly registered  
            //login id for Admin management 

            if(Emails.emails.Contains(user.Email.ToString()))
                await userManager.AddToRoleAsync(user, "Admin");
            await userManager.AddToRoleAsync(user, "User");
            var code = await userManager.GenerateEmailConfirmationTokenAsync(user);
            var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code =code}, protocol: HttpContext.Request.Scheme);
            var client = new SendGridClient(SendGridDetails.APIKEY);
            var msg = new SendGridMessage()
            {
                From = new EmailAddress("admin@tsrssocialinitiatives.com", "TSRS Social Initiatives"),
                Subject = "EmailConfirmation",
                HtmlContent = $"Please confirm your account by <a href='{callbackUrl}'>clicking here</a>.",
            };
            msg.AddTo(new EmailAddress(user.Email));

            // Disable click tracking.
            // See https://sendgrid.com/docs/User_Guide/Settings/tracking.html
            msg.SetClickTracking(false, false);
            await client.SendEmailAsync(msg);
            TempData["Message"] = "Successfully Registered. Confirm your email before sign in.";
            //await AppIdentityDbContext.AppUsers.AddAsync(user);
            //await AppIdentityDbContext.SaveChangesAsync();
            return Redirect("/Index/Home");
        }

        public async Task<IActionResult> Login (LoginModel login)
        {
            if(!ModelState.IsValid)
            {
                TempData["Message"] = "Error. Please try again.";
                return RedirectToAction("Home", "Index");
            }
            var user = await userManager.FindByEmailAsync(login.Email);
            if(!(await userManager.IsEmailConfirmedAsync(user)))
            {
                TempData["Message"] = "Confirm your email before sign in";
                return Redirect("/Index/Home");
            }
            if(user != null)
            {
                await signInManager.SignOutAsync();
                if((await signInManager.PasswordSignInAsync(user, login.Password, false, false)).Succeeded)
                {
                    return Redirect(login?.ReturnUrl ?? "/Index/Home");
                }
            }
            
            TempData["Message"] = "Invalid name or password";
            return Redirect("/Index/Home");
        }
        public async Task<IActionResult> LogOut ()
        {
            if(!ModelState.IsValid)
            {
                TempData["Message"] = "Error";
                return Redirect("/Index/Home");
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
                
                TempData["Message"] = "New password and confirm password do match";
                return Redirect("/Index/Home");
            }
            AppUser user = await userManager.GetUserAsync(HttpContext.User);
            IdentityResult result = await userManager.ChangePasswordAsync(user, model.currentPasword, model.newPassword);
            if(result.Succeeded)
                if((await userManager.UpdateAsync(user)).Succeeded)
                {
                    TempData["Message"] = "Password changed";
                    return RedirectToAction("Index", "UserAccount");
                }
            TempData["Message"] = "Error";
            return Redirect("/Index/Home"); 
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
            TempData["Message"] = "Error";
            return Redirect("/Index/Home");

        }
        public async Task<IActionResult> ConfirmEmailAsync(string userid, string code)
        {
            AppUser user = await userManager.FindByIdAsync(userid);
            IdentityResult result = await userManager.
                        ConfirmEmailAsync(user, code);
            
            if (result.Succeeded)
            {
                TempData["Message"] = "Email confirmed successfully!";
                return RedirectToAction("Home", "Index");
            }
            else
            {
                TempData["Message"] = "Error while confirming your email!";
                return View("Error");
            }
        }

        public IActionResult ResetPassword()
        {
            return View();
        }

        public async Task<IActionResult> ResetPasswordVAsync(PasswordResetViewModel vm)
        {
            AppUser user = await userManager.FindByEmailAsync(vm.email);
            var code = await userManager.GeneratePasswordResetTokenAsync(user);
            var callbackUrl = Url.Action("Reset", "Account", new { userId = user.Id, code = code }, protocol: HttpContext.Request.Scheme);
            var client = new SendGridClient(SendGridDetails.APIKEY);
            var msg = new SendGridMessage()
            {
                From = new EmailAddress("admin@tsrssocialinitiatives.com", "TSRS Social Initiatives"),
                Subject = "Password Recovery",
                HtmlContent = $"Change your password by <a href='{callbackUrl}'>clicking here</a>.",
            };
            msg.AddTo(new EmailAddress(user.Email));

            // Disable click tracking.
            // See https://sendgrid.com/docs/User_Guide/Settings/tracking.html
            msg.SetClickTracking(false, false);
            await client.SendEmailAsync(msg);
            TempData["Message"] = "Check your inbox for password reset link";
            return RedirectToAction("Home", "Index");
        }

        public IActionResult Reset(string userId, string code)
        {
            ViewBag.userId = userId;
            ViewBag.code = code;
            return View();
        }

        public async Task<IActionResult> ResetPasswordTokenAsync(PasswordResetViewModel viewModel)
        {
            if(viewModel.password == viewModel.Confirmpassword)
            {
            }
            else
            {
                ViewBag.userId = viewModel.userId;
                ViewBag.code = viewModel.code;
                TempData["Message"] = "Passwords do not match";
                return RedirectToAction("Reset", "Account");
            }
            AppUser user = await userManager.FindByIdAsync(viewModel.userId);
            IdentityResult result = await userManager.ResetPasswordAsync(user, viewModel.code, viewModel.password);
            if(result.Succeeded)
            {
                TempData["Message"] = "Password changed.";
                return RedirectToAction("Home", "Index");
            }
            else
            {
                TempData["Message"] = "Password not changed. Try again.";
                return RedirectToAction("Home", "Index");
            }
        }
    }

}