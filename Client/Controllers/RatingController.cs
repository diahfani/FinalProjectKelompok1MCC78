using Client.Models;
using Client.Repositories.Interface;
using Client.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Client.Controllers;

public class RatingController : Controller
{
    private readonly IRatingRepository ratrepository;
    private readonly ITaskRepository taskrepository;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IReportRepository _reportRepository;

    public RatingController(IRatingRepository _ratrepository, ITaskRepository _taskrepository, IHttpContextAccessor httpContextAccessor, IReportRepository reportRepository)
    {
        this.ratrepository = _ratrepository;
        this.taskrepository = _taskrepository;
        _httpContextAccessor = httpContextAccessor;
        _reportRepository = reportRepository;
    }

    public async Task<IActionResult> IndexEmployee()
    {
        var employeId = Guid.Parse(_httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier));
        var getTask = await taskrepository.GetTaskByEmployeeId(employeId);
        var listRating = new List<ReportRatingVM>();
        foreach (var item in getTask.Data)
        {
            var getReport = await _reportRepository.Get(item.Guid);
            var getRating = await ratrepository.Get(item.Guid);
            if (getReport.Data != null && getRating.Data != null)
            {
                var list = new ReportRatingVM
                {
                    Report = new Report
                    {
                        Guid = getReport.Data.Guid,
                        Subject = getReport.Data.Subject,
                        Description = getReport.Data.Description,
                        FileName = getReport.Data.FileName,
                        CreatedDate = getReport.Data.CreatedDate,
                        ModifiedDate = getReport.Data.ModifiedDate
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
                listRating.Add(list);
            } 
            else if (getReport.Data != null)
            {
                var list = new ReportRatingVM
                {
                    Report = new Report
                    {
                        Guid = getReport.Data.Guid,
                        Subject = getReport.Data.Subject,
                        Description = getReport.Data.Description,
                        FileName = getReport.Data.FileName,
                        CreatedDate = getReport.Data.CreatedDate,
                        ModifiedDate = getReport.Data.ModifiedDate
                    }
                };
                listRating.Add(list);
            }
            
        }
        return View(listRating);
    }

    public async Task<IActionResult> PerformanceEmployee()
    {
        var employeeId = Guid.Parse(_httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier));
        var getTask = await taskrepository.GetTaskByEmployeeId(employeeId);
        /*foreach(var item in getTask.Data)
        {
            var result = await ratrepository.Get(item.Guid);
            var result2 = await taskrepository.Get(item.Guid);
            var viewModel = new TaskRatingViewModel();

            if (result.Data != null)
            {
                viewModel.Ratings = new Rating
                {
                    Guid = result.Data.Guid,
                    RatingValue = result.Data.RatingValue,
                    Comment = result.Data.Comment,
                    CreatedDate = result.Data.CreatedDate,
                    ModifiedDate = result.Data.ModifiedDate
                };
            }
            if (result2.Data != null)
            {
                viewModel.Tasks = new Models.Task
                {
                    Guid = result2.Data.Guid,
                    Subject = result2.Data.Subject,
                    Description = result2.Data.Description,
                    Deadline = result2.Data.Deadline,
                    EmployeeGuid = result2.Data.EmployeeGuid,
                    CreatedDate = result2.Data.CreatedDate,
                    ModifiedDate = result2.Data.ModifiedDate

                };
            }
        }
        return View(viewModel);*/
        var result2 = await taskrepository.GetTaskByEmployeeId(employeeId);
        foreach(var item in result2.Data) { 
        }
        var result = await ratrepository.Get();
        var viewModel = new TaskRatingViewModel();

        if (result.Data != null)
        {
            viewModel.Ratings = result.Data.Select(e => new Rating
            {
                Guid = e.Guid,
                RatingValue = e.RatingValue,
                Comment = e.Comment,
                CreatedDate = e.CreatedDate,
                ModifiedDate = e.ModifiedDate
            }).ToList();
        }
        if (result2.Data != null)
        {
            viewModel.Tasks = result2.Data.Select(t => new Models.Task
            {
                Guid = t.Guid,
                Subject = t.Subject,
                Description = t.Description,
                Deadline = t.Deadline,
                EmployeeGuid = t.EmployeeGuid,
                CreatedDate = t.CreatedDate,
                ModifiedDate = t.ModifiedDate

            }).ToList();
        }
        return View(viewModel);


    }



    public async Task<IActionResult> StatusEmployee()
    {
        var employeId = Guid.Parse(_httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier));
        var getTask = await taskrepository.GetTaskByEmployeeId(employeId);
        var listRating = new List<ReportRatingVM>();
        foreach (var item in getTask.Data)
        {
            var getReport = await _reportRepository.Get(item.Guid);
            var getRating = await ratrepository.Get(item.Guid);
            if (getReport.Data != null && getRating.Data != null)
            {
                var list = new ReportRatingVM
                {
                    Report = new Report
                    {
                        Guid = getReport.Data.Guid,
                        Subject = getReport.Data.Subject,
                        Description = getReport.Data.Description,
                        FileName = getReport.Data.FileName,
                        CreatedDate = getReport.Data.CreatedDate,
                        ModifiedDate = getReport.Data.ModifiedDate
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
                listRating.Add(list);
            }
            else if (getReport.Data != null)
            {
                var list = new ReportRatingVM
                {
                    Report = new Report
                    {
                        Guid = getReport.Data.Guid,
                        Subject = getReport.Data.Subject,
                        Description = getReport.Data.Description,
                        FileName = getReport.Data.FileName,
                        CreatedDate = getReport.Data.CreatedDate,
                        ModifiedDate = getReport.Data.ModifiedDate
                    }
                };
                listRating.Add(list);
            }

        }
        return View(listRating);
    }


    [HttpGet]
    [Authorize(Roles ="manager")]
    public async Task<IActionResult> Creates()
    {
        Guid reportId = Guid.Parse(Request.Query["Guid"]);
        ViewData["ReportId"] = reportId;
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Creates(Rating rating)
    {
        var result = await ratrepository.Post(rating);
        if (result.Code == 200)
        {
            return Redirect("/Report/IndexManager");
        }
        else if (result.Code == 409)
        {
            ModelState.AddModelError(string.Empty, result.Message);
            return View();
        }

        return Redirect("/Report/IndexManager");
    }

    public async Task<IActionResult> Deletes(Guid guid)
    {
        var result = await ratrepository.Get(guid);
        var rating = new Rating();
        if (result.Data?.Guid is null)
        {
            return View(rating);
        }
        else
        {
            rating.Guid = result.Data.Guid;
            rating.RatingValue = result.Data.RatingValue;
            rating.Comment = result.Data.Comment;
            rating.CreatedDate = result.Data.CreatedDate;
            rating.ModifiedDate = result.Data.ModifiedDate;

        }
        return View(rating);
    }

    [HttpPost]
    public async Task<IActionResult> Remove(Guid guid)
    {
        var result = await ratrepository.Deletes(guid);
        if (result.Code == 200)
        {
            return RedirectToAction(nameof(Index));
        }
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    public async Task<IActionResult> Edit(Rating rating)
    {


        var result = await ratrepository.Put(rating);
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

    public async Task<IActionResult> Edit(Guid guid)
    {
        var result = await ratrepository.Get(guid);
        var rating = new Rating();
        if (result.Data?.Guid is null)
        {
            return View(rating);
        }
        else
        {
            rating.Guid = result.Data.Guid;
            rating.RatingValue = result.Data.RatingValue;
            rating.Comment = result.Data.Comment;
            rating.CreatedDate = result.Data.CreatedDate;
            rating.ModifiedDate = result.Data.ModifiedDate;
        }

        return View(rating);
    }
}

