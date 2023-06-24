using API.Contracts;
using API.Model;

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
}
