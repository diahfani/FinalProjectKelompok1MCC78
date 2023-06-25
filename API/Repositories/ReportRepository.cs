using API.Contracts;
using API.Model;
using API.Utilities;
using API.ViewModel.Employee;
using API.ViewModel.Report;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using System.Net.WebSockets;
using System.Runtime.InteropServices;

namespace API.Repositories;

public class ReportRepository : GeneralRepository<Report>, IReportRepository
{
    public ReportRepository(ProjectManagementDBContext context) : base(context)
    {
    }

    public async System.Threading.Tasks.Task DownloadFileByGuid(Guid guid)
    {
        try
        {
            var file = _context.Reports.Where(x => x.Guid == guid).FirstOrDefaultAsync();
            var content = new System.IO.MemoryStream(file.Result.FileData);
            var path = Path.Combine(Directory.GetCurrentDirectory(), "FileDownloaded", file.Result.FileName);
            await CopyStream(content, path);
        } catch(Exception)
        {
            throw;
        }
    }

    public async System.Threading.Tasks.Task CopyStream(Stream stream, string downloadPath)
    {
        using (var fileStream = new FileStream(downloadPath, FileMode.Create, FileAccess.Write))
        {
            await stream.CopyToAsync(fileStream);
        }
    }
    public async System.Threading.Tasks.Task PostFileAsync(FileUploadAndDownlodVM reportvm)
    {
        try
        {
            var fileDetails = new Report
            {
                Guid = reportvm.Guid,
                Subject = reportvm.Subject,
                Description = reportvm.Description,
                FileName = reportvm.FileName.FileName,
                FileType = reportvm.FileType,
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now
            };
            using (var stream = new MemoryStream())
            {
                reportvm.FileName.CopyTo(stream);
                fileDetails.FileData = stream.ToArray();
            }
            var result = _context.Reports.Add(fileDetails);
            await _context.SaveChangesAsync();
        } catch (Exception)
        {
            throw;
        }
    }

        public Report GetReportByEmployeeId(Guid employeeId)
        {
            return _context.Set<Report>().Find(employeeId);
        }
}

