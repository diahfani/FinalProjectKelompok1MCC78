using API.Contracts;
using API.Model;
using API.ViewModel.Employee;
using API.ViewModel.Rating;

namespace API.Repositories;

public class RatingRepository : GeneralRepository<Rating>, IRatingRepository
{
    private readonly IEmployeeRepository _employeeRepository;
    public RatingRepository(ProjectManagementDBContext context,     
                            IEmployeeRepository employeeRepository) : base(context)
    {
        _employeeRepository = employeeRepository;
    }

    public IEnumerable<RatingVM> GetRatingByEmployeeId(Guid employeeId)
    {
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
