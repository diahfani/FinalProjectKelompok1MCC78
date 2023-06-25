using API.Model;
using API.Utilities;
using API.ViewModel.Report;

namespace API.Contracts
{
    public interface IReportRepository : IGenericRepository<Report>
    {
        public System.Threading.Tasks.Task PostFileAsync(FileUploadAndDownlodVM report);
        public System.Threading.Tasks.Task DownloadFileByGuid(Guid guid);

        Report GetReportByEmployeeId(Guid employeeId);
    }
}
