using Client.Models;
using Client.ViewModels;

namespace Client.Repositories.Interface
{
    public interface IRatingRepository : IRepository<Rating, Guid>
    {
        public Task<ResponseListVM<Rating>> GetRating();

    }
}
