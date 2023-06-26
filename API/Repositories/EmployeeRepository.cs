using API.Contracts;
using API.Model;
using API.ViewModel.Employee;
using API.ViewModel.Task;
using Microsoft.Identity.Client;
using System.Data;

namespace API.Repositories;

public class EmployeeRepository : GeneralRepository<Employee>, IEmployeeRepository
{
    private readonly ITaskRepository _taskRepository;
    private readonly IReportRepository _reportRepository;
    private readonly IRatingRepository _ratingRepository;
    /*private readonly IEmployeeRepository _employeeRepository;*/
    public EmployeeRepository(ProjectManagementDBContext context,
                              ITaskRepository taskRepository,
                              IReportRepository reportRepository,
                              IRatingRepository ratingRepository) : base(context)
    {
        _taskRepository = taskRepository;
        _reportRepository = reportRepository;
        _ratingRepository = ratingRepository;
        /*_employeeRepository = employeeRepository;*/
    }

    public bool CheckEmailAndPhoneAndNIK(string value)
    {
        return _context.Employees.Any(e => e.Email == value || e.NIK == value || e.PhoneNumber == value);
    }



    public EmployeeVM GetByEmail(string email)
    {
        var employee = _context.Employees.FirstOrDefault(e => e.Email == email);
        var data = new EmployeeVM
        {
            Guid = employee.Guid,
            NIK = employee.NIK,
            Fullname = employee.Fullname,
            Gender = employee.Gender,
            HiringDate = employee.HiringDate,
            Email = employee.Email,
            PhoneNumber = employee.PhoneNumber,
            ManagerID = employee.ManagerID,
        };
        return data;

    }

    public Guid GetGuidByNIK(string NIK)
    {
        var employee = _context.Employees.FirstOrDefault(e => e.NIK == NIK);
        if (employee != null)
        {
            return employee.Guid;
        }
        return Guid.Empty;
    }

    public IEnumerable<EmployeeVM> GetEmployeeByManagerId(Guid managerId)
    {
        var employees = _context.Employees.Where(e => e.ManagerID == managerId).ToList();
        var employeeVM = new List<EmployeeVM>();

        foreach (var employee in employees)
        {
            var data = new EmployeeVM
            {
                Guid = employee.Guid,
                NIK = employee.NIK,
                Fullname = employee.Fullname,
                Gender = employee.Gender,
                HiringDate = employee.HiringDate,
                Email = employee.Email,
                PhoneNumber = employee.PhoneNumber,
                ManagerID = employee.ManagerID
            };
            employeeVM.Add(data);
        }
        return employeeVM;
    }

    public DetailVM GetDetailByEmployeeId(Guid employeeId)
    {
        var employee = _context.Employees.FirstOrDefault(e => e.Guid == employeeId);
        if (employee == null)
        {
            return null;
        }

        var tasks = _context.Tasks.FirstOrDefault(t => t.EmployeeGuid == employeeId);
        var report = _context.Reports.FirstOrDefault(r => r.Guid == tasks.Guid);
        var rating = _context.Ratings.FirstOrDefault(r => r.Guid == tasks.Guid);

        var data = new DetailVM
        {
            Guid = employee.Guid,
            NIK = employee.NIK,
            Fullname = employee.Fullname,
            Gender = employee.Gender,
            Email = employee.Email,
            PhoneNumber = employee.PhoneNumber,
            HiringDate = employee.HiringDate,
            ManagerID = employee.ManagerID,
            Subject = tasks.Subject,
            Description = tasks.Description,
            Deadline = tasks.Deadline,
            SubjectReport = report.Subject,
            DescriptionReport = report.Description,
            RatingValue = rating.RatingValue,
            Comment = rating.Comment,
            CreatedDate = employee.CreatedDate,
            ModifiedDate = employee.ModifiedDate
        };

        return data;
    }
}
