using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Client.Models;

namespace Client.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        public IActionResult LandingPage()
        {
            return View();
        }


        public IActionResult Login()
        {
            return View();
        }
        public IActionResult Register()
        {
            return View();
        }
        public IActionResult Manager()
        {
            return View();
        }
        public IActionResult Employee()
        {
            return View();
        }
        public IActionResult StatusManager()
        {
            return View();
        }

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