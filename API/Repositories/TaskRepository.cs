using API.Contracts;
using API.ViewModel.Employee;
using API.ViewModel.Task;

namespace API.Repositories;

public class TaskRepository : GeneralRepository<Model.Task>, ITaskRepository
{
    public TaskRepository(ProjectManagementDBContext context) : base(context)
    {
        
    }

    public IEnumerable<TaskVM> GetTaskByEmployeeId(Guid employeeId)
    {
        var tasks = _context.Tasks.Where(e => e.EmployeeGuid == employeeId).ToList();
        var taskVM = new List<TaskVM>();

        foreach (var task in tasks)
        {
            var data = new TaskVM
            {
                Guid = task.Guid,
                Subject = task.Subject,
                Description = task.Description,
                Deadline = task.Deadline,
                EmployeeGuid = task.EmployeeGuid
                
            };
            taskVM.Add(data);
        }
        return taskVM;
    }
}
