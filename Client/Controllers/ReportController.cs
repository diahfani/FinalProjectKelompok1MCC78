using Client.Models;
using Client.Repositories.Interface;
using Microsoft.AspNetCore.Mvc;

namespace Client.Controllers;

public class ReportController : Controller
{
    private readonly IReportRepository reprepository;

    public ReportController(IReportRepository _reprepository)
    {
        this.reprepository = _reprepository;
    }

    public async Task<IActionResult> Index()
    {
        var result = await reprepository.Get();
        var reports = new List<Report>();

        if (result.Data != null)
        {
            reports = result.Data.Select(e => new Report
            {
                Guid = e.Guid,
                Subject = e.Subject,
                Description = e.Description,
                File = e.File

            }).ToList();
        }
        return View(reports);
    }



    public async Task<IActionResult> Creates()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Creates(Report report)
    {
        var result = await reprepository.Post(report);
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
            report.File = result.Data.File;


        }
        return View(report);
    }

    [HttpPost]
    public async Task<IActionResult> Remove(Guid guid)
    {
        var result = await reprepository.Deletes(guid);
        if (result.StatusCode == "200")
        {
            return RedirectToAction(nameof(Index));
        }
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    public async Task<IActionResult> Edit(Report report)
    {


        var result = await reprepository.Put(report);
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
            report.File = result.Data.File;
        }

        return View(report);
    }
}

