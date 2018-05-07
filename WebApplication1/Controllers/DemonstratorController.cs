using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Models;
using WebApplication1.Models.DemonstratorViewModels;

namespace WebApplication1.Controllers
{
    [Authorize(Roles="Demonstrator")]
    public class DemonstratorController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;
        private readonly IHubContext<NotificationsHub> _hubContext;

        public DemonstratorController(UserManager<ApplicationUser> userManager, ApplicationDbContext context, IHubContext<NotificationsHub> hubContext)
        {
            _userManager = userManager;
            _context = context;
            _hubContext = hubContext;
        }
        public IActionResult Index()
        {
            var user = _userManager.GetUserAsync(User).Result;
            var helpRequests = _context.HelpRequest.Where(x => x.Status == Status.InProgress).Include(x=>x.StudentAndDemoUsers).ThenInclude(x=> x.User);
            var test = helpRequests.FirstOrDefault(x => x.StudentAndDemoUsers.FirstOrDefault(a => a.User == user) != null);
            var firstOrDefault = test?.StudentAndDemoUsers.FirstOrDefault(x => x.User != user);
            if (firstOrDefault == null) return View((HelpRequestListViewModel) null);
            var otherUser = firstOrDefault.User;
            var helpRequestViewModel = new HelpRequestListViewModel
            {
                DescOfProblem = test.HelpDesc,
                Id = test.Id,
                Location = otherUser.Location,
                StudentNumber = otherUser.StudentNumber,
                TimeRequested = test.DateCreated.ToString(CultureInfo.InvariantCulture)
            };
            return View(helpRequestViewModel);
        }

        public IActionResult AcceptRequest(int id)
        {
            var helpRequest = _context.HelpRequest.FirstOrDefault(x => x.Id == id && x.Status == Status.Requested);
            if (helpRequest == null) return RedirectToAction("Index");
            var user = _userManager.GetUserAsync(User).Result;
            var helpToUser = new UserToRequest {HelpRequest = helpRequest, User = user};
            _context.UserToRequest.Add(helpToUser);
            helpRequest.Status = Status.InProgress;
            _context.HelpRequest.Update(helpRequest);
            _context.SaveChanges();
            _hubContext.Clients.All.SendAsync("ReloadHelpList");
            return RedirectToAction("Index");
        }

        public IActionResult CompleteRequest(int id)
        {
            var helpRequest = _context.HelpRequest.FirstOrDefault(x => x.Id == id && x.Status == Status.InProgress);
            if (helpRequest != null)
            {
                helpRequest.Status = Status.Completed;
                _context.HelpRequest.Update(helpRequest);
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        public IActionResult GetListViewComponent()
        {
            return ViewComponent("HelpRequests");
        }
    }
}