using API.ViewModel.Employee;
using API.ViewModel.Task;

namespace API.Contracts
{
    public interface ITaskRepository : IGenericRepository<Model.Task>
    {
        public IEnumerable<TaskVM> GetTaskByEmployeeId(Guid employeeId);
    }
}
