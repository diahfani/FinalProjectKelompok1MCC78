using API.Model;
using API.ViewModel.Employee;

namespace API.Contracts
{
    public interface IEmployeeRepository : IGenericRepository<Employee>
    {
        EmployeeVM GetByEmail(string email);

    }
}
