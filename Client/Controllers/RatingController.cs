using Client.Models;
using Client.Repositories.Interface;
using Microsoft.AspNetCore.Mvc;

namespace Client.Controllers;

public class RatingController : Controller
{
    private readonly IRatingRepository ratrepository;

    public RatingController(IRatingRepository _ratrepository)
    {
        this.ratrepository = _ratrepository;
    }

    public async Task<IActionResult> Index()
    {
        var result = await ratrepository.Get();
        var ratings = new List<Rating>();

        if (result.Data != null)
        {
            ratings = result.Data.Select(e => new Rating
            {
                Guid = e.Guid,
                RatingValue = e.RatingValue,
                Comment = e.Comment
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

        }
        return View(rating);
    }

    [HttpPost]
    public async Task<IActionResult> Remove(Guid guid)
    {
        var result = await ratrepository.Deletes(guid);
        if (result.StatusCode == "200")
        {
            return RedirectToAction(nameof(Index));
        }
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    public async Task<IActionResult> Edit(Rating rating)
    {


        var result = await ratrepository.Put(rating);
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
        }

        return View(rating);
    }
}

