using Client.Models;
using Client.Repositories.Interface;
using Microsoft.AspNetCore.Mvc;
using Task = Client.Models.Task;

namespace Client.Controllers;

public class TaskController : Controller
{
    private readonly ITaskRepository tasrepository;

    public TaskController(ITaskRepository _tasrepository)
    {
        this.tasrepository = _tasrepository;
    }

    public async Task<IActionResult> Index()
    {
        var result = await tasrepository.Get();
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
        return View(tasks);
    }



    /*public async Task<IActionResult> Creates()
    {
        return View();
    }*/

    [HttpPost]
    public async Task<IActionResult> Creates(Task task)
    {
        var result = await tasrepository.Post(task);
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


        }
        return View(task);
    }

    [HttpPost]
    public async Task<IActionResult> Remove(Guid guid)
    {
        var result = await tasrepository.Deletes(guid);
        if (result.StatusCode == "200")
        {
            return RedirectToAction(nameof(Index));
        }
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    public async Task<IActionResult> Edit(Task task)
    {


        var result = await tasrepository.Put(task);
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
        }

        return View(task);
    }
}

