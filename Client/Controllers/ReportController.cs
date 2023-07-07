using Client.Models;
using Client.Repositories.Interface;
using Client.ViewModels;
using Microsoft.AspNetCore.Authorization;
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
    private readonly IRatingRepository _ratingRepository;

    public ReportController(IReportRepository _reprepository, IHttpContextAccessor httpContextAcessor, IEmployeeRepository emprepository, ITaskRepository tasrepository, IRatingRepository ratingRepository)
    {
        this.reprepository = _reprepository;
        _httpContextAcessor = httpContextAcessor;
        this.emprepository = emprepository;
        this.tasrepository = tasrepository;
        _ratingRepository = ratingRepository;
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

    [HttpGet]
    [Authorize(Roles = "manager")]
    public async Task<IActionResult> IndexManager()
    {
        var managerID = Guid.Parse(_httpContextAcessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier));
        var results = await emprepository.GetEmployeeByManagerID(managerID);
        var employeeId = new List<Guid>();
        var taskList = new List<Models.Task>();
        var taskEmp = new ViewModels.ResponseListVM<Models.Task>();
        var taskReportEmp = new List<TaskReportEmployeeVM>();
        var taskReportEmpRating = new List<TaskReportEmployeeRatingVM>();
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
            var getRating = await _ratingRepository.Get(j.Guid);
            if (getReport.Data != null && getRating.Data != null)
            {
                var taskListEmployee = new TaskReportEmployeeRatingVM
                {
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
                    },
                    Rating = new Rating
                    {
                        Guid = getRating.Data.Guid,
                        RatingValue = getRating.Data.RatingValue,
                        Comment = getRating.Data.Comment,
                        CreatedDate = getRating.Data.CreatedDate,
                        ModifiedDate = getRating.Data.ModifiedDate
                    }
                };
                taskReportEmpRating.Add(taskListEmployee);
            }
            else if (getReport.Data != null)
            {
                var taskListEmployee = new TaskReportEmployeeRatingVM
                {
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
                    },
                };
                taskReportEmpRating.Add(taskListEmployee);
            } else
            {
                var tasklistemployee = new TaskReportEmployeeRatingVM
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
                taskReportEmpRating.Add(tasklistemployee);
            }

        }
        /*foreach (var report in taskList)
        {
            var getReport = await reprepository.GetReportByTaskId(report.Guid);
            getReport ??= null;
        }*/
        return View(taskReportEmpRating);
    }

    [HttpGet]
    [Authorize(Roles = "employee")]
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
        var taskReportRating = new List<TaskReportRatingVM>();
        foreach (var task in taskList)
        {
            var getReport = await reprepository.Get(task.Guid);
            var getRating = await _ratingRepository.Get(task.Guid);
            if (getReport.Data != null && getRating.Data != null)
            {
                var taskreport = new TaskReportRatingVM
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
                    },
                    Rating = new Rating
                    {
                        Guid = getRating.Data.Guid,
                        RatingValue = getRating.Data.RatingValue,
                        Comment = getRating.Data.Comment,
                        CreatedDate = getRating.Data.CreatedDate,
                        ModifiedDate = getRating.Data.ModifiedDate
                    }
                };
                taskReportRating.Add(taskreport);
            }
            else if(getReport.Data != null) {
                var taskreport = new TaskReportRatingVM
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
                taskReportRating.Add(taskreport);

            }
            else
            {
                var taskreport = new TaskReportRatingVM
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
                taskReportRating.Add(taskreport);
            }
        }
        /*var result = await tasrepository.GetTaskByEmployeeId(employeeID);
        var reports = new List<Report>();
        foreach (var report in result.Data)
        {
            reports.Add(report);
        }*/
        return View(taskReportRating);
    }

    [HttpGet]
    [Authorize(Roles = "employee")]
    public async Task<IActionResult> Creates()
    {
        Guid taskId = Guid.Parse(Request.Query["Guid"]);
        ViewData["TaskId"] = taskId;
        /*var getTaskId = Guid.Parse(_httpContextAcessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier));
        ViewData["TaskId"] = getTaskId;*/
        return View();
    }

    [HttpPost]
    [Authorize(Roles = "employee")]
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
            return Redirect("/Report/IndexManager");
        }
        else if (result.Code == 409)
        {
            ModelState.AddModelError(string.Empty, result.Message);
            return Redirect("/Report/IndexManager");
        }

        return Redirect("/Report/IndexManager");
    }

    [HttpGet]
    [Authorize(Roles = "employee")]
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
            report.FileName = result.Data.FileName;
        }
        return View(report);
    }

    public async Task<IActionResult> Download()
    {
        return View();
    }

    [HttpPost("DownloadFile")]
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
    [Authorize(Roles = "employee")]
    public async Task<IActionResult> Remove(Guid guid)
    {
        var result = await reprepository.Deletes(guid);
        if (result.Message == "Delete Success")
        {
            return RedirectToAction(nameof(IndexEmployee));
        }
        return RedirectToAction(nameof(IndexEmployee));
    }

    [HttpPost]
    [Authorize(Roles = "employee")]
    public async Task<IActionResult> Edit(File file)
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
        var result = await reprepository.Put(fileDetails);
        if (result.Code == 200)
        {
            return RedirectToAction(nameof(IndexEmployee));
        }
        else if (result.Code == 409)
        {
            ModelState.AddModelError(string.Empty, result.Message);
            return View();
        }

        return RedirectToAction(nameof(IndexEmployee));
    }

    [HttpGet]
    [Authorize(Roles = "employee")]
    public async Task<IActionResult> Edit(Guid guid)
    {
        var result = await reprepository.Get(guid);
        var report = new File();
        if (result.Data?.Guid is null)
        {
            return View(report);
        }
        else
        {
            report.Guid = result.Data.Guid;
            report.Subject = result.Data.Subject;
            report.Description = result.Data.Description;
            report.FileType = result.Data.FileType;

        }

        return View(report);
    }
}

