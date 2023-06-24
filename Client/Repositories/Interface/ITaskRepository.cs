using Client.Models;
using Client.ViewModels;
using Task = Client.Models.Task;

namespace Client.Repositories.Interface;

public interface ITaskRepository : IRepository<Task, Guid>
{
    public Task<ResponseListVM<Task>> GetTask();

}