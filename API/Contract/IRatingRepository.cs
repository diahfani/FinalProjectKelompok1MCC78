using API.Model;

namespace API.Contracts
{
    public interface IRatingRepository : IGenericRepository<Rating>
    {
        Rating GetRatingByEmployeeId(Guid employeeId);
        Rating GetRatingByTaskId(Guid taskId);
    }
}
