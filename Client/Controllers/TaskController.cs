using Client.Models;
using Client.Repositories.Interface;
using Client.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Linq.Expressions;
using System.Security.Claims;
using Task = Client.Models.Task;

namespace Client.Controllers;

public class TaskController : Controller
{
    private readonly ITaskRepository tasrepository;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IEmployeeRepository emprepository;
    private readonly IReportRepository reprepository;

    public TaskController(ITaskRepository _tasrepository, IHttpContextAccessor httpContextAccessor, IEmployeeRepository emprepository, IReportRepository reprepository)
    {
        this.tasrepository = _tasrepository;
        _httpContextAccessor = httpContextAccessor;
        this.emprepository = emprepository;
        this.reprepository = reprepository;
    }

    public async Task<IActionResult> Index()
    {
        // ambil id manager
        var managerID = Guid.Parse(_httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier));
        // ambil employee yang manager id nya sma kayak id manager
        var results = await emprepository.GetEmployeeByManagerID(managerID);
        // buat list employee id
        var employeeId = new List<Guid>();
        // buat list task
        var taskList = new List<Task>();
        // buat list task employee
        var taskEmp = new ViewModels.ResponseListVM<Models.Task>();
        var taskListEmp = new List<TaskEmployeeVM>();
        // masukin employee id ke list employee id
        foreach (var i in results.Data)
        {
            employeeId.Add(i.Guid);
        }
        // masukin task ke list task
        foreach(var gettask in employeeId)
        {
            taskEmp = await tasrepository.GetTaskByEmployeeId(gettask);
            foreach(var task in taskEmp.Data)
            {
                taskList.Add(task);
            }
        }
        // masukin task ke list task employee
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

    public async Task<IActionResult> IndexEmployee()
    {
        var employeeID = Guid.Parse(_httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier));
        var result = await tasrepository.GetTaskByEmployeeId(employeeID);
        var tasks = new List<Task>();
        foreach (var task in result.Data)
        {
            tasks.Add(task);
        }
        var taskReport = new List<TaskReportVM>();
        foreach (var task in tasks)
        {
            var getReport = await reprepository.Get(task.Guid);
            if (getReport.Data != null)
            {
                var taskreport = new TaskReportVM
                {
                    Task = new Models.Task
                    {
                        Guid = task.Guid,
                        Subject = task.Subject,
                        Description = task.Description,
                        Deadline = task.Deadline,
                        EmployeeGuid = task.EmployeeGuid,
                        CreatedDate = task.CreatedDate,
                        ModifiedDate = task.ModifiedDate,
                    },
                    Report = new Report
                    {
                        Guid = getReport.Data.Guid,
                        Subject = getReport.Data.Subject,
                        Description = getReport.Data.Description,
                        FileName = getReport.Data.FileName,
                        FileType = getReport.Data.FileType,
                    }
                };
                taskReport.Add(taskreport);
            }
            else
            {
                var taskreport = new TaskReportVM
                {
                    Task = new Models.Task
                    {
                        Guid = task.Guid,
                        Subject = task.Subject,
                        Description = task.Description,
                        Deadline = task.Deadline,
                        EmployeeGuid = task.EmployeeGuid,
                        CreatedDate = task.CreatedDate,
                        ModifiedDate = task.ModifiedDate,
                    },

                };
                taskReport.Add(taskreport);
            }
        }

        return View(taskReport);
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
        if (result.Code == 200)
        {
            return Redirect("/Task/Creates");
        }
        else if (result.Code == 409)
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

