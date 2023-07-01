using API.Contracts;
using API.Model;
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
                            ITaskRepository taskRepository,
                            IEmployeeRepository employeeRepository,
                            IMapper<Rating, RatingVM> mapper) : base(ratingRepository, mapper)
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
            return BadRequest(new ResponseVM<RatingVM>
            {
                Code = StatusCodes.Status400BadRequest,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = "Rating Not Found"
            });
        }
        return Ok(new ResponseVM<IEnumerable<RatingVM>>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Found Data Rating",
            Data = ratings
        });
    }

    [HttpGet("GetRatingByTaskId")]
    public IActionResult GetRatingByTaskId(Guid taskId)
    {
        var ratings = _ratingRepository.GetRatingByTaskId(taskId);
        if (ratings == null)
        {
            return BadRequest(new ResponseVM<RatingVM>
            {
                Code = StatusCodes.Status400BadRequest,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = "Rating Not Found"
            });
        }
        return Ok(new ResponseVM<IEnumerable<RatingVM>>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Found Data Rating",
            Data = ratings
        });
    }
}
