using API.Model;
using API.ViewModel.Rating;

namespace API.Contracts
{
    public interface IRatingRepository : IGenericRepository<Rating>
    {
        IEnumerable<RatingVM> GetRatingByEmployeeId(Guid employeeId);
        IEnumerable<RatingVM> GetRatingByTaskId(Guid taskId);
    }
}
