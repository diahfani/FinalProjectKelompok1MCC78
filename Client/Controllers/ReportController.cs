using Client.Models;
using Client.Repositories.Interface;
using Client.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NuGet.Versioning;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Security.Claims;
using File = Client.Models.File;

namespace Client.Controllers;

public class ReportController : Controller
{
    private readonly IReportRepository reprepository;
    private readonly IHttpContextAccessor _httpContextAcessor;
    private readonly IEmployeeRepository emprepository;
    private readonly ITaskRepository tasrepository;

    public ReportController(IReportRepository _reprepository, IHttpContextAccessor httpContextAcessor, IEmployeeRepository emprepository, ITaskRepository tasrepository)
    {
        this.reprepository = _reprepository;
        _httpContextAcessor = httpContextAcessor;
        this.emprepository = emprepository;
        this.tasrepository = tasrepository;
    }

    public async Task<IActionResult> Index()
    {
        /*var employeeId = Guid.Parse(_httpContextAcessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier));
        var result = await tasrepository.GetTaskByEmployeeId(employeeId);
        var taskId = new List<Guid>();
        var reportList = new List<Report>();
        var taskRep = new ViewModels.ResponseListVM<ReportVM>();
        var reportlistEmp = new List<ReportTaskVM>();

        foreach (var i in result.Data)
        {
            taskId.Add(i.Guid);
        }
        foreach (var getreport in taskId)
        {
            taskRep = await reprepository.GetReportByTaskId(getreport);
            foreach (var rep in taskRep.Data)
            {
                reportList.Add(rep);
            }
        }*/
        return View();
    }

    public async Task<IActionResult> IndexManager()
    {
        var managerID = Guid.Parse(_httpContextAcessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier));
        var results = await emprepository.GetEmployeeByManagerID(managerID);
        var employeeId = new List<Guid>();
        var taskList = new List<Models.Task>();
        var taskEmp = new ViewModels.ResponseListVM<Models.Task>();
        var taskReportEmp = new List<TaskReportEmployeeVM>();
        foreach (var i in results.Data)
        {
            employeeId.Add(i.Guid);
        }
        foreach (var gettask in employeeId)
        {
            taskEmp = await tasrepository.GetTaskByEmployeeId(gettask);
            foreach (var task in taskEmp.Data)
            {
                taskList.Add(task);
            }
        }
        foreach (var j in taskList)
        {
            var getemp = await emprepository.Get(j.EmployeeGuid);
            var getReport = await reprepository.GetReportByTaskId(j.Guid);
            if (getReport.Data != null)
            {
                var tasklistemployee = new TaskReportEmployeeVM
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
                    },
                    Report = new ReportVM
                    {
                        Guid = getReport.Data.Guid,
                        Subject = getReport.Data.Subject,
                        Description = getReport.Data.Description,
                        FileName = getReport.Data.FileName
                    }
                };
                taskReportEmp.Add(tasklistemployee);
            }
            else
            {
                var tasklistemployee = new TaskReportEmployeeVM
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
                    },
                };
                taskReportEmp.Add(tasklistemployee);
            }

        }
        /*foreach (var report in taskList)
        {
            var getReport = await reprepository.GetReportByTaskId(report.Guid);
            getReport ??= null;
        }*/
        return View(taskReportEmp);
    }

    public async Task<IActionResult> IndexEmployee()
    {
        var employeeID = Guid.Parse(_httpContextAcessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier));
        var getTask = await tasrepository.GetTaskByEmployeeId(employeeID);
        var taskList = new List<Models.Task>();
        foreach (var task in getTask.Data)
        {
            taskList.Add(task);
        }
        var taskReport = new List<TaskReportVM>();
        foreach(var task in taskList)
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
        /*var result = await tasrepository.GetTaskByEmployeeId(employeeID);
        var reports = new List<Report>();
        foreach (var report in result.Data)
        {
            reports.Add(report);
        }*/
        return View(taskReport);
    }

    public async Task<IActionResult> Creates()
    {
        Guid taskId = Guid.Parse(Request.Query["Guid"]);
        ViewData["TaskId"] = taskId;
        /*var getTaskId = Guid.Parse(_httpContextAcessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier));
        ViewData["TaskId"] = getTaskId;*/
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Creates(File file)
    {
        var fileDetails = new Report
        {
            Guid = file.Guid,
            Subject = file.Subject,
            Description = file.Description,
            FileName = file.FileName.FileName,
            FileType = file.FileType,
            /*CreatedDate = DateTime.Now,
            ModifiedDate   = DateTime.Now,*/
        };
        using (var stream = new MemoryStream())
        {
            file.FileName.CopyTo(stream);
            fileDetails.FileData = stream.ToArray();
        }
        var result = await reprepository.Post(fileDetails);
        if (result.Message == "Create Success")
        {
            return RedirectToAction("/Report/Creates");
        }
        else if (result.StatusCode == 409)
        {
            ModelState.AddModelError(string.Empty, result.Message);
            return View();
        }

        return RedirectToAction("/Report/Creates");
    }

    public async Task<IActionResult> Deletes(Guid guid)
    {
        var result = await reprepository.Get(guid);
        var report = new Report();
        if (result.Data?.Guid is null)
        {
            return View(report);
        }
        else
        {
            report.Guid = result.Data.Guid;
            report.Subject = result.Data.Subject;
            report.Description = result.Data.Description;


        }
        return View(report);
    }

    public async Task<IActionResult> Download()
    {
        return View();
    }





    [HttpPost]
    public async Task<IActionResult> DownloadFile(Guid reportId)
    {
        var result = await reprepository.DownloadReport(reportId);
        if (result == "OK")
        {
            return RedirectToAction(nameof(IndexManager));
        }
        return RedirectToAction(nameof(IndexManager));
    }

    [HttpPost]
    public async Task<IActionResult> Remove(Guid guid)
    {
        var result = await reprepository.Deletes(guid);
        if (result.StatusCode == 200)
        {
            return RedirectToAction(nameof(Index));
        }
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    public async Task<IActionResult> Edit(File file)
    {


        var result = await reprepository.Put(file);
        if (result.StatusCode == 200)
        {
            return RedirectToAction(nameof(Index));
        }
        else if (result.StatusCode == 409)
        {
            ModelState.AddModelError(string.Empty, result.Message);
            return View();
        }

        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> Edit(Guid guid)
    {
        var result = await reprepository.Get(guid);
        var report = new Report();
        if (result.Data?.Guid is null)
        {
            return View(report);
        }
        else
        {
            report.Guid = result.Data.Guid;
            report.Subject = result.Data.Subject;
            report.Description = result.Data.Description;
        }

        return View(report);
    }
}

