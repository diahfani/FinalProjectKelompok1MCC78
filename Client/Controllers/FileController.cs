using Client.Models;
using Client.Repositories.Interface;
using Microsoft.AspNetCore.Mvc;
using File = Client.Models.File;

namespace Client.Controllers;

public class FileController : Controller
{
    private readonly IFileRepository filerepository;

    public FileController(IFileRepository _filerepository)
    {
        this.filerepository = _filerepository;
    }

    public async Task<IActionResult> Index()
    {
        var result = await filerepository.Get();
        var files = new List<File>();

        if (result.Data != null)
        {
            files = result.Data.Select(e => new File
            {
                Guid = e.Guid,
                Name = e.Name,
                Data = e.Data,
                Type = e.Type

            }).ToList();
        }
        return View(files);
    }



    /*public Task<IActionResult> Creates()
    {
        return View();
    }*/

    [HttpPost]
    public async Task<IActionResult> Creates(File file)
    {
        var result = await filerepository.Post(file);
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
        var result = await filerepository.Get(guid);
        var file = new File();
        if (result.Data?.Guid is null)
        {
            return View(file);
        }
        else
        {
            file.Guid = result.Data.Guid;
            file.Name = result.Data.Name;
            file.Data = result.Data.Data;
            file.Type = result.Data.Type;


        }
        return View(file);
    }

    [HttpPost]
    public async Task<IActionResult> Remove(Guid guid)
    {
        var result = await filerepository.Deletes(guid);
        if (result.StatusCode == "200")
        {
            return RedirectToAction(nameof(Index));
        }
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    public async Task<IActionResult> Edit(File file)
    {


        var result = await filerepository.Put(file);
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
        var result = await filerepository.Get(guid);
        var file = new File();
        if (result.Data?.Guid is null)
        {
            return View(file);
        }
        else
        {
            file.Guid = result.Data.Guid;
            file.Name = result.Data.Name;
            file.Data = result.Data.Data;
            file.Type = result.Data.Type;
        }

        return View(file);
    }
}

