﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace SocialInitiatives3.Controllers
{
    
    public class ErrorController : Controller
    {
        [Route("error")]
        public IActionResult Index()
        {
            return View();
        }
    }
}