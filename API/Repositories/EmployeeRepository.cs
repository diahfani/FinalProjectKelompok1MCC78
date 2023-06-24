using API.Contracts;
using API.Model;
using API.ViewModel.Employee;
using System.Data;

namespace API.Repositories;

public class EmployeeRepository : GeneralRepository<Employee>, IEmployeeRepository
{
        public EmployeeRepository(ProjectManagementDBContext context) : base(context)
        {
             
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
}
