using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
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

        private readonly IHostingEnvironment _hostingEnviroment;

        public StudentController(
            UserManager<ApplicationUser> userManager,
            ILogger<AccountController> logger,
            ApplicationDbContext context,
            IHubContext<NotificationsHub> hubContext,
            IHostingEnvironment hostingEnviroment)
        {
            _userManager = userManager;
            _logger = logger;
            _context = context;
            _hubContext = hubContext;
            _hostingEnviroment = hostingEnviroment;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            //gives ::1 if running on localhost and says throws an exception
            var user = _userManager.GetUserAsync(User).Result;

            
            var sessionStart = _context.LabSession.FirstOrDefault(x => x.SessionComplete == false);
            if (sessionStart == null)
                ViewData["SessionStarted"] = false;
            else
            {
                user.Location = GetLocation(sessionStart.RoomName);
                await _userManager.UpdateAsync(user);
                ViewData["TestIp"] = user.Location;
                ViewData["SessionStarted"] = true;
            }
            return View();
        }
        private string GetLocation(string roomName)
        {
            var result = "Unknown Location";
            var jsonString = GetJsonString();
            var jsonObject = JsonConvert.DeserializeObject<LocationsClass>(jsonString);
            var locationList = jsonObject.Locations.FirstOrDefault(x => x.LocationName == roomName);
            if (locationList == null) return result;
            var ip = Request.HttpContext.Connection.RemoteIpAddress.ToString();
            var seatList = locationList.Seats.FirstOrDefault(x => x.Ip == ip);
            if (seatList == null) return result;
            var seat = seatList.SeatNo;
            result = seat;
            return result;
        }

        private string GetJsonString()
        {
            var contentRoot = _hostingEnviroment.ContentRootPath;
            var result = System.IO.File.ReadAllText(contentRoot + "/Locations.json");
            return result;
        }
        [HttpPost]
        public async Task<IActionResult> Index(HelpRequestViewModel model, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            ViewData["SessionStarted"] = true;
            if (!ModelState.IsValid) return View(model);
            var helpRequest = new HelpRequest { HelpDesc = model.Description };
            var user = _userManager.GetUserAsync(User).Result;
            var userToReq = new UserToRequest { HelpRequest = helpRequest, User = user };
            _context.HelpRequest.Add(helpRequest);
            _context.UserToRequest.Add(userToReq);
            await _context.SaveChangesAsync();
            _logger.LogInformation("Added Help Request");
            ModelState.Clear();
            await _hubContext.Clients.All.SendAsync("ReloadHelpList");
            return View();
        }

    }
}