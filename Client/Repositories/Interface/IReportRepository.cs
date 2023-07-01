using Client.Models;
using Client.ViewModels;

namespace Client.Repositories.Interface
{
    public interface IReportRepository : IRepository<Report, Guid>
    {
        public Task<ResponseListVM<Report>> GetReport();
        public Task<ResponseViewModel<ReportVM>> GetReportByTaskId(Guid TaskId);

        public Task<string> DownloadReport(Guid ReportId);

    }
}
