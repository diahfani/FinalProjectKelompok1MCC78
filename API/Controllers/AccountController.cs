using API.Contracts;
using API.Model;
using API.Repositories;
using API.Utilities;
using API.ViewModel.Account;
using API.ViewModel.Other;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Net;
using System.Security.Claims;
using API.Utilities;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]

public class AccountController : BaseController<Account, AccountVM>
{
    private readonly IAccountRepository _accountRepository;
    private readonly IEmployeeRepository _employeeRepository;
    private readonly ITokenService _tokenService;
    public AccountController(IAccountRepository accountRepository, IEmployeeRepository employeeRepository, ITokenService tokenService, IMapper<Account, AccountVM> mapper) : base(accountRepository, mapper)
    {
        _accountRepository = accountRepository;
        _employeeRepository = employeeRepository;
        _tokenService = tokenService;
    }

    [HttpPost("Register")]
    [AllowAnonymous]
    public IActionResult Register(RegisterVM registerVM)
    {
        var result = _accountRepository.Register(registerVM);
        switch (result)
        {
            case 0:
                return BadRequest(new ResponseVM<RegisterVM>
                {
                    Code = StatusCodes.Status400BadRequest,
                    Status = HttpStatusCode.BadRequest.ToString(),
                    Message = "Registration failed",
                    Data = null
                }); ;
            case 1:
                return BadRequest(new ResponseVM<RegisterVM>
                {
                    Code = StatusCodes.Status400BadRequest,
                    Status = HttpStatusCode.BadRequest.ToString(),
                    Message = "Email already exist",
                    Data = null
                });
            case 2:
                return BadRequest(new ResponseVM<RegisterVM>
                {
                    Code = StatusCodes.Status400BadRequest,
                    Status = HttpStatusCode.BadRequest.ToString(),
                    Message = "Phone number already exist",
                    Data = null
                });
            case 3:
                return Ok(new ResponseVM<RegisterVM>
                {
                    Code = StatusCodes.Status200OK,
                    Status = HttpStatusCode.OK.ToString(),
                    Message = "Success register!",
                    Data = null
                });
        }

        return BadRequest(new ResponseVM<RegisterVM>
        {
            Code = StatusCodes.Status400BadRequest,
            Status = HttpStatusCode.BadRequest.ToString(),
            Message = "Failed",
            Data = null
        });

    }

    [HttpPost("login")]
    [AllowAnonymous]
    public IActionResult Login(LoginVM loginVM)
    {
        var query = _accountRepository.Login(loginVM);
        var employees = _employeeRepository.GetByEmail(loginVM.Email);
        if (employees == null)
        {
            return BadRequest(new ResponseVM<LoginVM>
            {
                Code = StatusCodes.Status400BadRequest,
                Status = HttpStatusCode.BadRequest.ToString(),
                Message = "Employee not found"
            });
        }

        if (query == null)
        {
            return BadRequest(new ResponseVM<LoginVM>
            {
                Code = StatusCodes.Status400BadRequest,
                Status = HttpStatusCode.BadRequest.ToString(),
                Message = "Email not found",
                Data = null
            });
        }
        var validatePassword = Hashing.ValidatePassword(loginVM.Password, query.Password);
        if (validatePassword is false)
        {
            return BadRequest(new ResponseVM<LoginVM>
            {
                Code = StatusCodes.Status400BadRequest,
                Status = HttpStatusCode.BadRequest.ToString(),
                Message = "Password didn't match",
                Data = null
            });
        }

        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, employees.NIK),
            new(ClaimTypes.Name, $"{employees.Fullname}"),
            new(ClaimTypes.Email, employees.Email)
        };

        var roles = _accountRepository.GetRoles(employees.Guid);

        foreach (var role in roles)
        {
            claims.Add(new Claim(ClaimTypes.Role, role));
        }

        var token = _tokenService.GenerateToken(claims);

        return Ok(new ResponseVM<string>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Success login!",
            Data = token
        });

    }

}
