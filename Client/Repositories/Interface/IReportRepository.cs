using Client.Models;
using Client.ViewModels;

namespace Client.Repositories.Interface
{
    public interface IReportRepository : IRepository<Report, Guid>
    {
        public Task<ResponseListVM<ReportVM>> GetReport();
        public Task<ResponseViewModel<Models.Report>> GetReportByTaskId(Guid TaskId);
        public Task<string> DownloadReport(Guid ReportId);
        public Task<ResponseViewModel<Models.File>> Post(Models.File file);
        public Task<ResponseViewModel<Models.File>> Put(Models.File file);
    }
}
