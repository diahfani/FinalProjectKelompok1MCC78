using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Client.Models;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using System.Runtime.CompilerServices;

namespace Client.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public HomeController(ILogger<HomeController> logger, IHttpContextAccessor httpContextAccessor)
        {
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
        }
        [AllowAnonymous]
        public IActionResult LandingPage()
        {
            string jwToken = HttpContext.Session.GetString("JWToken") ?? "JWT is null";
            ViewData["JWToken"] = jwToken;
            var role = _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.Role);
            ViewData["Role"] = role;
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
            return View("Views/Home/Manager.cshtml");
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
        [AllowAnonymous]
        [HttpGet("/Unauthorized")]
        public IActionResult Unauthorized()
        {
            return View("401");
        }

        [AllowAnonymous]
        [HttpGet("/Forbidden")]
        public IActionResult Forbidden()
        {
            return View("403");
        }

        [AllowAnonymous]
        [HttpGet("/NotFound")]
        public IActionResult NotFound()
        {
            return View("404");
        }
    }
}