﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SocialInitiatives3.Infrastructure;
using SocialInitiatives3.Models;
using SocialInitiatives3.Models.ViewModels;

namespace SocialInitiatives3.Controllers
{
    public class NGOController : Controller
    {
        private AppDbContext _dbContext
            ;

        public NGOController(AppDbContext context)
        {
            _dbContext = context;
        }

        [Route("[controller]")]
        [Route("[controller]/[action]")]
        public IActionResult Index()
        {
            ViewBag.SelectedNav = "NGO";
            return View();
        }

        [Route("[controller]/[action]/{id?}")]
        public IActionResult Category(string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            int i = 0;
            var success = Int32.TryParse(id, out i);
            if (!success)
            {
                return RedirectToAction("Index", "Calendar");
            }
            //RegisterModel rm = new RegisterModel();
            //rm.initiatives = _dbContext.initiatives.Where(j => j.categoryId == i);
            ViewBag.SelectedNav = "Initiatives";
            ViewBag.Title = CategoryDict.Categories[i];
            return View();
        }
    }
}