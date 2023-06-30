using API.Contracts;
using API.Model;
using API.ViewModel.Rating;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RatingController : BaseController<Rating, RatingVM>
{
    private readonly IRatingRepository _ratingRepository;
    public RatingController(IRatingRepository ratingRepository, IMapper<Rating, RatingVM> mapper)
        : base(ratingRepository, mapper)
    {
        _ratingRepository = ratingRepository;
    }
}
