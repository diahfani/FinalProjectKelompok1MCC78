namespace API.Contracts
{
    public interface ITaskRepository : IGenericRepository<Model.Task>
    {
        Task GetByEmployeeId(Guid employeeId);
    }
}
