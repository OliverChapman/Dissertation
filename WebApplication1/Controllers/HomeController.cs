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
            ILogger logger)
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
            if (await _userManager.IsInRoleAsync(user, "Demonstrator"))
            {
                _logger.LogInformation("User is Demonstrator.");
                return RedirectToAction(nameof(DemonstratorController.Index), "Demonstrator");
            }
            if (!await _userManager.IsInRoleAsync(user, "Student")) throw new Exception("User has no Role");
            _logger.LogInformation("User is Student.");
            return RedirectToAction(nameof(StudentController.Index), "Student");
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
