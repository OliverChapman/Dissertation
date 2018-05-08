using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Models;
using WebApplication1.Models.LecturerViewModels;

namespace WebApplication1.Controllers
{
    [Authorize(Roles="Lecturer")]
    public class LecturerController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;
        private readonly IHubContext<NotificationsHub> _hubContext;
        public LecturerController(UserManager<ApplicationUser> userManager, ApplicationDbContext context, IHubContext<NotificationsHub> hubContext)
        {
            _userManager = userManager;
            _context = context;
            _hubContext = hubContext;
        }
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            var labSessionList = _context.LabSession.Where(x => x.SessionComplete == false).Include(x => x.Users);
            var userLabSessList = labSessionList.FirstOrDefault(x => x.Users.FirstOrDefault(a => a == user) != null);
            return View(userLabSessList);
        }

        [HttpPost]
        public async Task<IActionResult> CreateLabSession(AddLabRequestViewModel model)
        {
            if (!ModelState.IsValid) return ViewComponent("AddLabSession", model);
            var labReq = new LabSession
            {
                ModuleName = model.ModuleName,
                ModuleNo = model.ModuleNo,
                RoomName = model.RoomName,
                Users = new List<ApplicationUser>()
            };
            var user = _userManager.GetUserAsync(User).Result;
            labReq.Users.Add(user);
            _context.LabSession.Add(labReq);
            await _context.SaveChangesAsync();
            await _hubContext.Clients.All.SendAsync("ReloadStudentPage");
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> CompleteLab(int id)
        {
            var labSess = _context.LabSession.Find(id);
            if (labSess == null) return RedirectToAction("Index");
            labSess.SessionComplete = true;
            _context.LabSession.Update(labSess);
            //TODO: should add a way that saves all of help requests to client pc before wiping
            _context.Database.ExecuteSqlCommand("DELETE FROM dbo.UserToRequest");
            _context.Database.ExecuteSqlCommand("DELETE FROM dbo.HelpRequest");
            _context.UserToRequest.RemoveRange();
            await _context.SaveChangesAsync();
            var demoUsers = await _userManager.GetUsersInRoleAsync("Demonstrator");
            foreach (var user in demoUsers)
            {
                await _userManager.RemoveFromRoleAsync(user, "Demonstrator");
                await _userManager.AddToRoleAsync(user, "Student");
                user.UserType = UserType.Student;
                await _userManager.UpdateAsync(user);
            }
            await _hubContext.Clients.All.SendAsync("SignOutEveryone");
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> MakeStudentDemonstrator(string id)
        {
            var userToDemo =await _userManager.FindByIdAsync(id);
            await _userManager.RemoveFromRoleAsync(userToDemo, "Student");
            await _userManager.AddToRoleAsync(userToDemo, "Demonstrator");
            userToDemo.UserType = UserType.Demonstrator;
            await _userManager.UpdateAsync(userToDemo);
            return RedirectToAction("Index");
            //ajax if you have time
            //return ViewComponent("CreateDemonstrator");
        }

        public async Task<IActionResult> RemoveStudentDemonstrator(string id)
        {
            var userToDemo = await _userManager.FindByIdAsync(id);
            await _userManager.RemoveFromRoleAsync(userToDemo, "Demonstrator");
            await _userManager.AddToRoleAsync(userToDemo, "Student");
            userToDemo.UserType = UserType.Student;
            await _userManager.UpdateAsync(userToDemo);         
            return RedirectToAction("Index");
            //ajax if you have time
            //return ViewComponent("CreateDemonstrator");
        }
    }
}