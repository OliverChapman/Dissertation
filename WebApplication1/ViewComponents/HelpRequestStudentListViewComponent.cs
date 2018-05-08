using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Models;
using WebApplication1.Models.DemonstratorViewModels;
using WebApplication1.Models.StudentViewModels;

namespace WebApplication1.ViewComponents
{
    public class HelpRequestStudentListViewComponent : ViewComponent
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;

        public HelpRequestStudentListViewComponent(UserManager<ApplicationUser> userManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var helpToUserList = _context.UserToRequest.Where(x => x.User == user).Include(x => x.HelpRequest).ToList();
            var result = helpToUserList.Select(helpToUser => new HelpRequestListItemViewModel
                {
                    DateRequested = helpToUser.HelpRequest.DateCreated,
                    Problem = helpToUser.HelpRequest.HelpDesc,
                    Status = helpToUser.HelpRequest.Status
                })
                .ToList();
            return View(result);
        }
    }
}
