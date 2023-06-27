using Client.Models;
using Client.Repositories.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Client.Controllers;

public class EmployeeController : Controller
{
    private readonly IEmployeeRepository emprepository;

    public EmployeeController(IEmployeeRepository _emprepository)
    {
        this.emprepository = _emprepository;
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



    /*public Task<IActionResult> Creates()
    {
        return View();
    }*/

    [HttpPost]
    public async Task<IActionResult> Creates(Employee employee)
    {
        var result = await emprepository.Post(employee);
        if (result.StatusCode == "200")
        {
            return RedirectToAction(nameof(Index));
        }
        else if (result.StatusCode == "409")
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
        if (result.StatusCode == "200")
        {
            return RedirectToAction(nameof(Index));
        }
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    public async Task<IActionResult> Edit(Employee employee)
    {


        var result = await emprepository.Put(employee);
        if (result.StatusCode == "200")
        {
            return RedirectToAction(nameof(Index));
        }
        else if (result.StatusCode == "409")
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
