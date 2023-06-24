using API.Contracts;
using API.Model;
using API.ViewModel.Employee;
using API.ViewModel.Other;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace API.Controllers
{
    public class EmployeeController : BaseController<Employee, EmployeeVM>
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMapper<Employee, EmployeeVM> _mapper;

        public EmployeeController(IEmployeeRepository employeeRepository, IMapper<Employee, EmployeeVM> mapper) : base(employeeRepository, mapper)
        {
            _employeeRepository = employeeRepository;
            _mapper = mapper;
        }

    }
}
