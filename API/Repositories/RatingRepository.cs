using API.Contracts;
using API.Model;
using API.ViewModel.Employee;

namespace API.Repositories;

public class RatingRepository : GeneralRepository<Rating>, IRatingRepository
{
    public RatingRepository(ProjectManagementDBContext context) : base(context)
    {
    }

    public Rating GetRatingByEmployeeId(Guid employeeId)
    {
        return _context.Set<Rating>().Find(employeeId);
    }

    public Rating GetRatingByTaskId(Guid taskId)
    {
        return _context.Set<Rating>().Find(taskId);
    }
}
