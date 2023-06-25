using API.Model;
using API.ViewModel.Employee;

namespace API.Contracts
{
    public interface IEmployeeRepository : IGenericRepository<Employee>
    {
        EmployeeVM GetByEmail(string email);
        public IEnumerable<EmployeeVM> GetEmployeeByManagerId(Guid managerId);
        public DetailVM? GetDetailByEmployeeId(Guid employeeId);
        Guid GetGuidByNIK(string NIK);
            
    }
}
