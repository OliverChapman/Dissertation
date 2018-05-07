using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using WebApplication1.Data;
using WebApplication1.Models;
using WebApplication1.Models.StudentViewModels;

namespace WebApplication1.Controllers
{
    [Authorize(Roles = "Student,Demonstrator")]
    public class StudentController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger _logger;
        private readonly ApplicationDbContext _context;
        //private IHttpContextAccessor _accessor;
        private readonly IHubContext<NotificationsHub> _hubContext;

        public StudentController(
            UserManager<ApplicationUser> userManager,
            ILogger<AccountController> logger,
            ApplicationDbContext context,
            IHubContext<NotificationsHub> hubContext)
        {
            _userManager = userManager;
            _logger = logger;
            _context = context;
            _hubContext = hubContext;
        }

        [HttpGet]
        public IActionResult Index(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            //gives ::1 if running on localhost and says throws an exception
            var user = _userManager.GetUserAsync(User).Result;
            ViewData["TestIp"] = user.Location;
            return View();
        }

        [HttpPost]
        public IActionResult Index(HelpRequestViewModel model, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            if (!ModelState.IsValid) return View(model);
            var helpRequest = new HelpRequest {HelpDesc = model.Description};
            var user = _userManager.GetUserAsync(User).Result;
            var userToReq = new UserToRequest {HelpRequest = helpRequest, User = user};
            _context.HelpRequest.Add(helpRequest);
            _context.UserToRequest.Add(userToReq);
            _context.SaveChanges();
            _logger.LogInformation("Added Help Request");
            ModelState.Clear();
            _hubContext.Clients.All.SendAsync("ReloadHelpList");
        return View();
        }

    }
    }