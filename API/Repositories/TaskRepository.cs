using API.Contracts;

namespace API.Repositories;

public class TaskRepository : GeneralRepository<Model.Task>, ITaskRepository
{
    public TaskRepository(ProjectManagementDBContext context) : base(context)
    {
        
    }
}
