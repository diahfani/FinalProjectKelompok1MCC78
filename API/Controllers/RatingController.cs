﻿using API.Contracts;
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
<<<<<<< Updated upstream
    public RatingController(IRatingRepository ratingRepository, 
                            IMapper<Rating, RatingVM> mapper,
                            ITaskRepository taskRepository,
                            IEmployeeRepository employeeRepository) : base(ratingRepository, mapper)
=======
    public RatingController(IRatingRepository ratingRepository,
                            ITaskRepository taskRepository,
                            IEmployeeRepository employeeRepository,
                            IMapper<Rating, RatingVM> mapper)
                            : base(ratingRepository, mapper)
>>>>>>> Stashed changes
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
<<<<<<< Updated upstream
            return NotFound(new ResponseVM<RatingVM>
            {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = "Not Found"
=======
            return NotFound(new ResponseVM<IEnumerable<RatingVM>>
            {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = "Rating Not Found"
>>>>>>> Stashed changes
            });
        }
        return Ok(new ResponseVM<IEnumerable<RatingVM>>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
<<<<<<< Updated upstream
            Message = "Found Data Employee",
=======
            Message = "Rating Found",
>>>>>>> Stashed changes
            Data = ratings
        });
    }

    [HttpGet("GetRatingByTaskId")]
    public IActionResult GetRatingByTaskId(Guid taskId)
    {
        var ratings = _ratingRepository.GetRatingByTaskId(taskId);
        if (ratings == null)
        {
<<<<<<< Updated upstream
            return NotFound(new ResponseVM<RatingVM>
            {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = "Not Found"
=======
            return NotFound(new ResponseVM<IEnumerable<RatingVM>>
            {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = "Rating Not Found"
>>>>>>> Stashed changes
            });
        }
        return Ok(new ResponseVM<IEnumerable<RatingVM>>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
<<<<<<< Updated upstream
            Message = "Found Data Employee",
=======
            Message = "Rating Found",
>>>>>>> Stashed changes
            Data = ratings
        });
    }
}
