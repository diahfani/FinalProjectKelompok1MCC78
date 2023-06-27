using API.Contracts;
using API.Model;
using API.Utilities;
using API.ViewModel.Employee;
using API.ViewModel.Report;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using System.Net.WebSockets;
using System.Runtime.InteropServices;
using System.Security.Principal;

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

    public Report GetReportByTaskId(Guid TaskId)
    {
        var report = _context.Reports.FirstOrDefault(r => r.Task.Guid == TaskId);
        return report;
    }

    public IEnumerable<ReportVM> GetReportByEmployeeId(Guid employeeId)
    {
        var reports = _context.Reports.Where(r => r.Task.EmployeeGuid == employeeId)
                                      .Select(r => new ReportVM
        {
            Guid = r.Guid,
            SubjectReport = r.Subject,
            DescriptionReport = r.Description,
            FileName = r.FileName,
            FileData = r.FileData,
            FileType = r.FileType,
            CreatedDate = r.CreatedDate,
            ModifiedDate = r.ModifiedDate
        })
        .ToList();
        return reports;
    }

    public async System.Threading.Tasks.Task UpdateReport(FileUploadAndDownlodVM report)
    {
        try
        {
            var guid = (Guid)typeof(FileUploadAndDownlodVM).GetProperty("Guid").GetValue(report);
            var oldentity = GetByGuid(guid);
            if (oldentity == null)
            {
                return;
            }
/*            var getCreatedDate = typeof(FileUploadAndDownlodVM).GetProperty("CreatedDate")!.GetValue(oldentity)!;
            typeof(FileUploadAndDownlodVM).GetProperty("CreatedDate")!.SetValue(report, getCreatedDate);
*/            typeof(FileUploadAndDownlodVM).GetProperty("ModifiedDate")!.SetValue(report, DateTime.Now);

            var fileDetails = new Report
            {
                Guid = report.Guid,
                Subject = report.Subject,
                Description = report.Description,
                FileName = report.FileName.FileName,
                FileType = report.FileType,
                CreatedDate = oldentity.CreatedDate,
            };
            using (var stream = new MemoryStream())
            {
                report.FileName.CopyTo(stream);
                fileDetails.FileData = stream.ToArray();
            }
            var result = _context.Set<Report>().Update(fileDetails);
            await _context.SaveChangesAsync();

        }
        catch (Exception)
        {
            throw;
        }
    }
}

