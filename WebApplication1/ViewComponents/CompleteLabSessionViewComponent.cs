using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Models;

namespace WebApplication1.ViewComponents
{
    public class CompleteLabSessionViewComponent : ViewComponent
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        public CompleteLabSessionViewComponent(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var labSessionList = _context.LabSession.Where(x => x.SessionComplete == false).Include(x=> x.Users);
            var userLabSessList = labSessionList.Where(x => x.Users.FirstOrDefault(a=> a == user) != null);
            return View(userLabSessList);
        }
    }
}
