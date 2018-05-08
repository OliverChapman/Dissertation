using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;
using WebApplication1.Models.LecturerViewModels;

namespace WebApplication1.ViewComponents
{
    public class CreateDemonstratorViewComponent : ViewComponent
    {
        private readonly UserManager<ApplicationUser> _userManager;
        public CreateDemonstratorViewComponent(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var role =await _userManager.GetUsersInRoleAsync("Student");
            var studentUserList = role.Select(user => new StudentUserListViewModel
                {
                    Id = user.Id,
                    FirstName = user.Forename,
                    StudentNumber = user.StudentNumber,
                    Surname = user.Surname
                })
                .OrderBy(x=> x.StudentNumber).ToList();
            return View(studentUserList);
        }
    }
}
