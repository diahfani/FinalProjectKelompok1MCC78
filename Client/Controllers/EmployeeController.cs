using Client.Models;
using Client.Repositories.Interface;
using Client.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Client.Controllers;

public class EmployeeController : Controller
{
    private readonly IEmployeeRepository emprepository;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public EmployeeController(IEmployeeRepository _emprepository, IHttpContextAccessor http)
    {
        this.emprepository = _emprepository;
        _httpContextAccessor = http;
    }

    public async Task<IActionResult> Index()
    {
        var result = await emprepository.Get();
        var employees = new List<Employee>();

        if (result.Data != null)
        {
            employees = result.Data.Select(e => new Employee
            {
                Guid = e.Guid,
                NIK = e.NIK,
                Fullname = e.Fullname,
                Gender = e.Gender,
                Email = e.Email,
                PhoneNumber = e.PhoneNumber,
                HiringDate = e.HiringDate,
                CreatedDate = e.CreatedDate,
                ModifiedDate = e.ModifiedDate,
                ManagerID = e.ManagerID
            }).ToList();
        }
        return View(employees);
    }

    [HttpGet]
    [Authorize(Roles = "manager")]
    public async Task<IActionResult> Manager()
    {
        /*var managerGUID = Guid.Parse(User.Claims.)*/
        var managerID = Guid.Parse(_httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier));
        var result = await emprepository.GetEmployeeByManagerID(managerID);
        var employeeManager = new List<Employee>();

        if(result.Data!= null)
        {
            employeeManager = result.Data.Select(e => new Employee
            {
                Guid = e.Guid,
                NIK = e.NIK,
                Fullname = e.Fullname,
                Gender = e.Gender,
                Email = e.Email,
                PhoneNumber = e.PhoneNumber,
                HiringDate = e.HiringDate,
                CreatedDate = e.CreatedDate,
                ModifiedDate = e.ModifiedDate,
                ManagerID = e.ManagerID
            }).ToList();
        }
        return View(employeeManager);
    }
    /*public Task<IActionResult> Creates()
    {
        return View();
    }*/

    [HttpGet]
    [Authorize(Roles = "employee")]
    public async Task<IActionResult> Employee()
    {
        /*var managerGUID = Guid.Parse(User.Claims.)*/
        var employeeId = Guid.Parse(_httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier));
        var result = await emprepository.Get(employeeId);
        /*var employeeManager = new List<Employee>();*/

        if (result.Data != null)
        {
            var employeeDetails =  new Employee
            {
                Guid = result.Data.Guid,
                NIK = result.Data.NIK,
                Fullname = result.Data.Fullname,
                Gender = result.Data.Gender,
                Email = result.Data.Email,
                PhoneNumber = result.Data.PhoneNumber,
                HiringDate = result.Data.HiringDate,
                CreatedDate = result.Data.CreatedDate,
                ModifiedDate = result.Data.ModifiedDate,
                ManagerID = result.Data.ManagerID
            };
            return View(employeeDetails);
        }
        return View();
    }


    [HttpPost]
    public async Task<IActionResult> Creates(Employee employee)
    {
        var result = await emprepository.Post(employee);
        if (result.Code == 200)
        {
            return RedirectToAction(nameof(Index));
        }
        else if (result.Code == 409)
        {
            ModelState.AddModelError(string.Empty, result.Message);
            return View();
        }

        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Deletes(Guid guid)
    {
        var result = await emprepository.Get(guid);
        var employee = new Employee();
        if (result.Data?.Guid is null)
        {
            return View(employee);
        }
        else
        {
            employee.Guid = result.Data.Guid;
            employee.NIK = result.Data.NIK;
            employee.Fullname = result.Data.Fullname;
            employee.Gender = result.Data.Gender;
            employee.Email = result.Data.Email;
            employee.PhoneNumber = result.Data.PhoneNumber;
            employee.HiringDate = result.Data.HiringDate;
            employee.CreatedDate = result.Data.CreatedDate;
            employee.ModifiedDate = result.Data.ModifiedDate;
            employee.ManagerID = result.Data.ManagerID;
        }
        return View(employee);
    }

    [HttpPost]
    public async Task<IActionResult> Remove(Guid guid)
    {
        var result = await emprepository.Deletes(guid);
        if (result.Code == 200)
        {
            return RedirectToAction(nameof(Index));
        }
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    public async Task<IActionResult> Edit(Employee employee)
    {


        var result = await emprepository.Put(employee);
        if (result.Code == 200)
        {
            return RedirectToAction(nameof(Index));
        }
        else if (result.Code == 409)
        {
            ModelState.AddModelError(string.Empty, result.Message);
            return View();
        }

        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> Edit(Guid guid)
    {
        var result = await emprepository.Get(guid);
        var employee = new Employee();
        if (result.Data?.Guid is null)
        {
            return View(employee);
        }
        else
        {
            employee.Guid = result.Data.Guid;
            employee.NIK = result.Data.NIK;
            employee.Fullname = result.Data.Fullname;
            employee.Gender = result.Data.Gender;
            employee.Email = result.Data.Email;
            employee.PhoneNumber = result.Data.PhoneNumber;
            employee.HiringDate = result.Data.HiringDate;
            employee.CreatedDate = result.Data.CreatedDate;
            employee.ModifiedDate = result.Data.ModifiedDate;
            employee.ManagerID = result.Data.ManagerID;
        }

        return View(employee);
    }
}
