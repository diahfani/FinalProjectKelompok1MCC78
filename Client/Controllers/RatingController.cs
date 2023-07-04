using Client.Models;
using Client.Repositories.Interface;
using Microsoft.AspNetCore.Mvc;

namespace Client.Controllers;

public class RatingController : Controller
{
    private readonly IRatingRepository ratrepository;
    private readonly ITaskRepository taskrepository;

    public RatingController(IRatingRepository _ratrepository, ITaskRepository _taskrepository)
    {
        this.ratrepository = _ratrepository;
        this.taskrepository = _taskrepository;
    }

    public async Task<IActionResult> PerformanceEmployee()
    {
        var result = await ratrepository.Get();
        var result2 = await taskrepository.Get();
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
        var result = await ratrepository.Get();
        var ratings = new List<Rating>();

        if (result.Data != null)
        {
            ratings = result.Data.Select(e => new Rating
            {
                Guid = e.Guid,
                RatingValue = e.RatingValue,
                Comment = e.Comment,
                CreatedDate = e.CreatedDate,
                ModifiedDate = e.ModifiedDate
            }).ToList();
        }
        return View(ratings);
    }



    public async Task<IActionResult> Creates()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Creates(Rating rating)
    {
        var result = await ratrepository.Post(rating);
        if (result.Code == 200)
        {
            return RedirectToAction("StatusManager", "Home");
        }
        else if (result.Code == 409)
        {
            ModelState.AddModelError(string.Empty, result.Message);
            return View();
        }

        return RedirectToAction("StatusManager", "Home");
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

