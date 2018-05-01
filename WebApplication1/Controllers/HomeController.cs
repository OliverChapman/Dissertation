using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger _logger;

        public HomeController(
            UserManager<ApplicationUser> userManager,
            ILogger<AccountController> logger)
        {
            _userManager = userManager;
            _logger = logger;
        }
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return View();
            }
            switch (user.UserType)
            {
                case UserType.Demonstrator:
                    _logger.LogInformation("User is Demonstrator.");
                    return RedirectToAction(nameof(DemonstratorController.Index), "Demonstrator");
                case UserType.Student:
                    _logger.LogInformation("User is Student.");
                    return RedirectToAction(nameof(StudentController.Index), "Student");
                case UserType.Lecturer:
                    _logger.LogInformation("User is Lecturer");
                    //return RedirectToAction(nameof(LecturerController.Index), "Lecturer");
                    return null;
            }

            throw new Exception("User has no Role");
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
