using API.Contracts;
using API.Model;
using API.ViewModel.Employee;
using API.ViewModel.Other;
using API.ViewModel.Rating;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RatingController : BaseController<Rating, RatingVM>
{
    private readonly IRatingRepository _ratingRepository;
    private readonly ITaskRepository _taskRepository;
    private readonly IEmployeeRepository _employeeRepository;
    public RatingController(IRatingRepository ratingRepository, 
                            IMapper<Rating, RatingVM> mapper,
                            ITaskRepository taskRepository,
                            IEmployeeRepository employeeRepository) : base(ratingRepository, mapper)
    {
        _ratingRepository = ratingRepository;
        _taskRepository = taskRepository;
        _employeeRepository = employeeRepository;
    }

    [HttpGet("GetRatingByEmployeeId")]
    public IActionResult GetRatingByEmployeeId(Guid employeeId)
    {
        var ratings = _ratingRepository.GetRatingByEmployeeId(employeeId);
        if (ratings == null)
        {
            return NotFound(new ResponseVM<RatingVM>
            {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = "Not Found"
            });
        }
        return Ok(new ResponseVM<IEnumerable<RatingVM>>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Found Data Employee",
            Data = ratings
        });
    }

    [HttpGet("GetRatingByTaskId")]
    public IActionResult GetRatingByTaskId(Guid taskId)
    {
        var ratings = _ratingRepository.GetRatingByTaskId(taskId);
        if (ratings == null)
        {
            return NotFound(new ResponseVM<RatingVM>
            {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = "Not Found"
            });
        }
        return Ok(new ResponseVM<IEnumerable<RatingVM>>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Found Data Employee",
            Data = ratings
        });
    }
}
