﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers
{
    [Authorize(Roles="Demonstrator")]
    public class DemonstratorController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}