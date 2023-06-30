using Client.Models;
using Client.Repositories.Interface;
using Client.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;
using System.Security.Claims;
using Task = Client.Models.Task;

namespace Client.Controllers;

public class TaskController : Controller
{
    private readonly ITaskRepository tasrepository;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IEmployeeRepository emprepository;

    public TaskController(ITaskRepository _tasrepository, IHttpContextAccessor httpContextAccessor, IEmployeeRepository emprepository)
    {
        this.tasrepository = _tasrepository;
        _httpContextAccessor = httpContextAccessor;
        this.emprepository = emprepository;
    }

    public async Task<IActionResult> Index()
    {
        var managerID = Guid.Parse(_httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier));
        var results = await emprepository.GetEmployeeByManagerID(managerID);
        var employeeId = new List<Guid>();
        var taskList = new List<Task>();
        var taskEmp = new ViewModels.ResponseListVM<Models.Task>();
        var taskListEmp = new List<TaskEmployeeVM>();
        foreach (var i in results.Data)
        {
            employeeId.Add(i.Guid);
        }
        foreach(var gettask in employeeId)
        {
            taskEmp = await tasrepository.GetTaskByEmployeeId(gettask);
            foreach(var task in taskEmp.Data)
            {
                taskList.Add(task);
            }
        }
        foreach(var j in taskList)
        {
            var getemp = await emprepository.Get(j.EmployeeGuid);
            var tasklistemployee = new TaskEmployeeVM
            {
                Guid = j.Guid,
                Subject = j.Subject,
                Description = j.Description,
                Deadline = j.Deadline,
                EmployeeGuid = j.EmployeeGuid,
                CreatedDate = j.CreatedDate,
                ModifiedDate = j.ModifiedDate,
                Employee = new EmployeeVM
                {
                    Guid = getemp.Data.Guid,
                    NIK = getemp.Data.NIK,
                    Fullname = getemp.Data.Fullname,
                    Gender = getemp.Data.Gender,
                    Email = getemp.Data.Email,
                    PhoneNumber = getemp.Data.PhoneNumber,
                    HiringDate = getemp.Data.HiringDate,
                    CreatedDate = getemp.Data.CreatedDate,
                    ModifiedDate = getemp.Data.ModifiedDate,
                    ManagerID = getemp.Data.ManagerID
                }
            };
            taskListEmp.Add(tasklistemployee);
        }
/*        Console.Write(taskEmp);
        Console.Write(taskList);*/
        return View(taskListEmp);
        /*var result = await tasrepository.Get();
        var tasks = new List<Task>();

        if (result.Data != null)
        {
            tasks = result.Data.Select(e => new Task
            {
                Guid = e.Guid,
                Subject = e.Subject,
                Description = e.Description,
                Deadline = e.Deadline

            }).ToList();
        }
        return View(tasks);*/
    }


   /* [HttpGet]
    [Authorize(Roles = "manager")]*/
    public async Task<IActionResult> Creates()
    {
        var managerID = Guid.Parse(_httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier));
        var result = await emprepository.GetEmployeeByManagerID(managerID);
        var employeeManager = new List<Employee>();

        if (result.Data != null)
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
        ViewBag.EmployeeManager = employeeManager;
        return View();
    }

    [HttpPost]
    /*[Authorize(Roles = "manager")]*/
    public async Task<IActionResult> Creates(Task task)
    {
        var result = await tasrepository.Post(task);
        if (result.StatusCode == 200)
        {
            return Redirect("/Task/Creates");
        }
        else if (result.StatusCode == 409)
        {
            ModelState.AddModelError(string.Empty, result.Message);
            return View();
        }

        return Redirect("/Task/Creates");
    }

    public async Task<IActionResult> Deletes(Guid guid)
    {
        var result = await tasrepository.Get(guid);
        var task = new Task();
        if (result.Data?.Guid is null)
        {
            return View(task);
        }
        else
        {
            task.Guid = result.Data.Guid;
            task.Subject = result.Data.Subject;
            task.Description = result.Data.Description;
            task.Deadline = result.Data.Deadline;
            task.EmployeeGuid = result.Data.EmployeeGuid;
            task.ModifiedDate = result.Data.ModifiedDate;
            task.CreatedDate = result.Data.CreatedDate;

        }
        return View(task);
    }

    [HttpPost]
    public async Task<IActionResult> Remove(Guid guid)
    {
        var result = await tasrepository.Deletes(guid);
        if (result.Message == "Delete Success")
        {
            return RedirectToAction(nameof(Index));
        }
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    public async Task<IActionResult> Edit(Task task)
    {


        var result = await tasrepository.Put(task);
        if (result.Message == "Update success")
        {
            return RedirectToAction(nameof(Index));
        }
        else if (result.Message != "Update success")
        {
            ModelState.AddModelError(string.Empty, result.Message);
            return View();
        }

        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> Edit(Guid guid)
    {
        var managerID = Guid.Parse(_httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier));
        var resultManager = await emprepository.GetEmployeeByManagerID(managerID);
        var employeeManager = new List<Employee>();

        if (resultManager.Data != null)
        {
            employeeManager = resultManager.Data.Select(e => new Employee
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
        ViewBag.EmployeeManager = employeeManager;

        var result = await tasrepository.Get(guid);
        var task = new Task();
        if (result.Data?.Guid is null)
        {
            return View(task);
        }
        else
        {
            task.Guid = result.Data.Guid;
            task.Subject = result.Data.Subject;
            task.Description = result.Data.Description;
            task.EmployeeGuid = result.Data.EmployeeGuid;
            task.Deadline = result.Data.Deadline;
            task.ModifiedDate = result.Data.ModifiedDate;
            task.CreatedDate = result.Data.CreatedDate;
        }

        return View(task);
    }
}

