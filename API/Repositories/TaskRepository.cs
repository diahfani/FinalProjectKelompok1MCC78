using API.Contracts;

namespace API.Repositories;

public class TaskRepository : GeneralRepository<Model.Task>, ITaskRepository
{
    public TaskRepository(ProjectManagementDBContext context) : base(context)
    {
        
    }

    public Task GetByEmployeeId(Guid employeeId)
    {
        return _context.Set<Task>().Find(employeeId);
    }
}
