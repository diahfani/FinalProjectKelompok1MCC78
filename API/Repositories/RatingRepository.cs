using API.Contracts;
using API.Model;
using API.ViewModel.Employee;
using API.ViewModel.Rating;

namespace API.Repositories;

public class RatingRepository : GeneralRepository<Rating>, IRatingRepository
{
    public RatingRepository(ProjectManagementDBContext context) : base(context)
    {
    }

    public IEnumerable<RatingVM> GetRatingByEmployeeId(Guid employeeId)
    {
        var getTaskbyEmployee = _context.Set<Model.Task>().Where(e => e.EmployeeGuid == employeeId).ToList();
        var ratings = _context.Set<Rating>().Where(r => r.Guid == employeeId).ToList();
        var ratingVM = new List<RatingVM>();

        foreach (var rating in ratings)
        {
            var data = new RatingVM
            {
                Guid = rating.Guid,
                RatingValue = rating.RatingValue,
                Comment = rating.Comment,
                CreatedDate = rating.CreatedDate,
                ModifiedDate = rating.ModifiedDate
            };
            ratingVM.Add(data);
        }
        return ratingVM;
    }
    public IEnumerable<RatingVM> GetRatingByTaskId(Guid taskId)
    {
        var tasks = _context.Set<Model.Task>().Where(t => t.Guid == taskId);
        var ratings = _context.Set<Rating>().Where(r => r.Guid == taskId).ToList();
        var ratingsVM = new List<RatingVM>();

        tasks ??= null;
        foreach (var rating in ratings)
        {
            var data = new RatingVM
            {
                Guid = rating.Guid,
                RatingValue = rating.RatingValue,
                Comment = rating.Comment,
                CreatedDate = rating.CreatedDate,
                ModifiedDate = rating.ModifiedDate,
            };
            ratingsVM.Add(data);
        }
        return ratingsVM;
    }
}
