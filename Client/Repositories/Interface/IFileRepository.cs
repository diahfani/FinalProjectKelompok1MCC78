using Client.Models;
using Client.ViewModels;
using File = Client.Models.File;

namespace Client.Repositories.Interface
{
    public interface IFileRepository : IRepository<File, Guid>
    {
        public Task<ResponseListVM<File>> GetFile();

    }
}
