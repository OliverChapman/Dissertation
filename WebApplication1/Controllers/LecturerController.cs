using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Models;
using WebApplication1.Models.LecturerViewModels;

namespace WebApplication1.Controllers
{
    public class LecturerController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;
        public LecturerController(UserManager<ApplicationUser> userManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _context = context;
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
            return RedirectToAction("Index");
        }

        public IActionResult CompleteLab(int id)
        {
            var labSess = _context.LabSession.Find(id);
            labSess.SessionComplete = true;
            _context.LabSession.Update(labSess);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}