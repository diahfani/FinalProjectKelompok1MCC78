﻿using Microsoft.AspNetCore.Mvc;
using Client.Repositories.Interface;
using Client.ViewModels;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authorization;

namespace Client.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountRepository repository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IAccountRoleRepository _accountRoleRepository;

        public AccountController(IAccountRepository repository, IHttpContextAccessor httpContextAccessor, IAccountRoleRepository accountRoleRepository)
        {
            this.repository = repository;
            _httpContextAccessor = httpContextAccessor;
            _accountRoleRepository = accountRoleRepository;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Logins()
        {
            if (HttpContext.Session.GetString("JWToken") != null)
            {
                var role = _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.Role);
                if (role == "manager")
                {
                    return RedirectToAction("Manager", "Account");
                }
                else if (role == "employee")
                {
                    return RedirectToAction("Employee", "Home");
                }
            }
            return View();
        }

        [HttpPost]
        /*[ValidateAntiForgeryToken]*/
        public async Task<IActionResult> Logins(LoginVM login)
        {
            if (HttpContext.Session.GetString("JWToken") != null)
            {
                var role = _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.Role);
                if (role == "manager")
                {
                    return RedirectToAction("Manager", "Employee");
                }
                else if (role == "employee")
                {
                    return RedirectToAction("Employee", "Employee");
                }
            }
            var result = await repository.Logins(login);
            var token = result.Data;
            var claims = ExtractClaims(token);
            var getRole =  "";
/*            Console.WriteLine(claims);
*/            foreach(var claim in claims)
            {
                if (claim.Type == "http://schemas.microsoft.com/ws/2008/06/identity/claims/role")
                {
                    getRole = claim.Value;
                   /* Console.WriteLine($"Claim Type: {claim.Type} - Claim Value: {claim.Value}");
                    HttpContext.Session.SetString("Role", claim.Value);*/
                }
                
            }
/*            Console.WriteLine(getRole);
*/            if (result is null)
            {
                return RedirectToAction("Error", "Home");
            }
            else if (result.Code == 409)
            {
                ModelState.AddModelError(string.Empty, result.Message);
                return View();
            }
            if (result.Code == 200)
            {
                HttpContext.Session.SetString("JWToken", result.Data);
                /*return RedirectToAction("LandingPage", "Home");*/
                /*var role = _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.Role);*/
                if (getRole == "manager")
                {
                    return RedirectToAction("Manager", "Employee");
                }
                else if (getRole == "employee")
                {
                    return RedirectToAction("Employee", "Employee");
                }
            }
            return View();

        }

        public IEnumerable<Claim> ExtractClaims(string jwtToken)
        {
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            JwtSecurityToken securityToken = (JwtSecurityToken)tokenHandler.ReadToken(jwtToken);
            IEnumerable<Claim> claims = securityToken.Claims;
            return claims;
        }

        [HttpGet("/Logout")]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return Redirect("/Account/Logins");
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Registers()
        {
            var getRoleManager = await _accountRoleRepository.GetRoleManager();
            ViewBag.RoleManager = getRoleManager.Data;
            return View();
        }

        [HttpPost]
        /*[ValidateAntiForgeryToken]*/
        public async Task<IActionResult> Registers(RegisterVM registerVM)
        {

            var result = await repository.Registers(registerVM);
            if (result is null)
            {
                return RedirectToAction("Error", "Home");
            }
            else if (result.Code == 409)
            {
                ModelState.AddModelError(string.Empty, result.Message);
                TempData["Error"] = $"Something Went Wrong! - {result.Message}!";
                return View();
            }
            else if (result.Code == 200)
            {
                TempData["Success"] = $"Data has been Successfully Registered! - {result.Message}!";
                return RedirectToAction("Logins", "Account");
            }
            return RedirectToAction("LandingPage", "Home");
        }
    }
}
