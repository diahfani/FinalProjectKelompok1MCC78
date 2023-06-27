using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Client.Models;
using Microsoft.AspNetCore.Authorization;

namespace Client.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        [AllowAnonymous]
        public IActionResult LandingPage()
        {
            return View();
        }

        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }
        [AllowAnonymous]
        public IActionResult Register()
        {
            return View();
        }
        [Authorize(Roles = "manager")]
        public IActionResult Manager()
        {
            return View();
        }
        [Authorize(Roles = "employee")]
        public IActionResult Employee()
        {
            return View("Views/Home/Employee.cshtml");
        }
        [Authorize(Roles = "manager")]
        public IActionResult StatusManager()
        {
            return View();
        }
        [Authorize(Roles = "employee")]
        public IActionResult StatusEmployee()
        {
            return View();
        }

[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}