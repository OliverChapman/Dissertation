﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers
{
    [Authorize(Roles="Student,Demonstrator")]
    public class StudentController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}