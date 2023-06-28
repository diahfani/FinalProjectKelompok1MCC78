using API.Contracts;
using API.Model;
using API.Utilities;
using API.ViewModel.Employee;
using API.ViewModel.Other;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace API.Controllers
{
    public class EmployeeController : BaseController<Employee, EmployeeVM>
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly ITaskRepository _taskRepository;
        private readonly IReportRepository _reportRepository;
        private readonly IRatingRepository _ratingRepository;
        private readonly IMapper<Employee, EmployeeVM> _mapper;

        public EmployeeController(IEmployeeRepository employeeRepository,
                                  ITaskRepository taskRepository,
                                  IReportRepository reportRepository,
                                  IRatingRepository ratingRepository,
                                  IMapper<Employee, EmployeeVM> mapper) : base(employeeRepository, mapper)
        {
            _employeeRepository = employeeRepository;
            _taskRepository = taskRepository;
            _reportRepository = reportRepository;
            _ratingRepository = ratingRepository;
            _mapper = mapper;
        }

        [HttpGet("GetEmployeeByManagerId")]
        /*[Authorize(Roles = $"{nameof(RoleLevel.manager)}")]*/
        public IActionResult GetEmployeeById(Guid managerId)
        {
            var employees = _employeeRepository.GetEmployeeByManagerId(managerId);
            if (employees == null)
            {
                return NotFound(new ResponseVM<EmployeeVM>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Not Found"
                });
            }
            return Ok(new ResponseVM<IEnumerable<EmployeeVM>>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Found Data Employee",
                Data = employees
            });
        }

        [HttpGet("GetDetailByEmployeeId")]
        public IActionResult GetDetailByEmployeeId(Guid employeeId)
        {
            var detail = _employeeRepository.GetDetailByEmployeeId(employeeId);
            if (detail == null)
            {
                return NotFound(new ResponseVM<DetailVM>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Employee Not Found"
                });
            }

            return Ok(new ResponseVM<DetailVM>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Employee Details Found",
                Data = detail
            });
        }

        [HttpGet("GetGuidByNIK")]
        public IActionResult GetGuidByNIK(string NIK)
        {
            var employees = _employeeRepository.GetGuidByNIK(NIK);
            if (employees == null)
            {
                return NotFound(new ResponseVM<EmployeeVM>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Not Found"
                });
            }

            var employee = new EmployeeVM
            {
                Guid = employees,
                NIK = NIK
            };

            return Ok(new ResponseVM<EmployeeVM>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Found Data Detail Employee",
                Data = employee
            });
        }

        [HttpGet("GetByEmail")]
        public IActionResult GetByEmail(string email)
        {
            var employees = _employeeRepository.GetByEmail(email);
            if (employees == null)
            {
                return NotFound(new ResponseVM<EmployeeVM>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Not Found"
                });
            }

            var employee = new EmployeeVM
            {
                Guid = employees.Guid,
                NIK = employees.NIK,
                Fullname = employees.Fullname,
                Gender = employees.Gender,
                HiringDate = employees.HiringDate,
                Email = employees.Email,
                PhoneNumber = employees.PhoneNumber,
                ManagerID = employees.ManagerID,
            };

            return Ok(new ResponseVM<EmployeeVM>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Found Data Detail Employee",
                Data = employee
            });
        }


    }
}
