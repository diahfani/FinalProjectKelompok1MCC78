using API.Contracts;
using API.Model;
using API.Utilities;
using API.ViewModel.Other;
using API.ViewModel.Report;
using Microsoft.AspNetCore.Authorization;
using API.ViewModel.Employee;
using API.ViewModel.Other;
using API.ViewModel.Report;
using API.ViewModel.Task;
using Azure;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;

namespace API.Controllers;

public class ReportController : BaseController<Report, ReportVM>
{
    private readonly IReportRepository _reportRepository;
    private readonly ITaskRepository _taskRepository;

    public ReportController(IReportRepository reportRepository,
                            ITaskRepository taskRepository,
                            IMapper<Report, ReportVM> mapper) : base(reportRepository, mapper)
    {
        _reportRepository = reportRepository;
        _taskRepository = taskRepository;
    }

    [HttpPost("PostSingleFile")]
    /*[Authorize(Roles = $"{nameof(RoleLevel.employee)}")]*/
    public async Task<IActionResult> PostSingleFile([FromForm] FileUploadAndDownlodVM reportvm)
    {
        if (reportvm is null)
        {
            return BadRequest();
        }

        try
        {
            await _reportRepository.PostFileAsync(reportvm);
            return Ok();
        } catch (Exception)
        {
            throw;
        }
    }

    [HttpPut("UpdateSingleReport")]
    /*[Authorize(Roles = $"{nameof(RoleLevel.employee)}")]*/
    public async Task<IActionResult> UpdateSingleReport([FromForm] FileUploadAndDownlodVM reportvm)
    {
        if (reportvm is null)
        {
            return BadRequest();
        }

        try
        {
            await _reportRepository.UpdateReport(reportvm);
            return Ok();
        }
        catch (Exception)
        {
            throw;
        }
    }

    [HttpGet("DownloadFile")]
    /*[Authorize(Roles = $"{nameof(RoleLevel.employee)}")]*/
    public async Task<IActionResult> DownloadFile(Guid guid)
    {
        if (guid == null)
        {
            return BadRequest();
        }

        try
        {
            await _reportRepository.DownloadFileByGuid(guid);
            return Ok();
        } catch (Exception)
        {
            throw;
        }
    }

    [HttpGet("GetReportByTaskId")]
    public IActionResult GetReportByTaskId(Guid taskId)
    {
        try
        {
            var report = _reportRepository.GetReportByTaskId(taskId);
            if (report == null)
            {
                return NotFound(new ResponseVM<ReportVM>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Not Found"
                });
            }

            var reportVM = new ReportVM
            {
                Guid = taskId,
                Subject = report.Subject,
                Description = report.Description,
                FileName = report.FileName,
                FileData = report.FileData,
                FileType = report.FileType,
                CreatedDate = report.CreatedDate,
                ModifiedDate = report.ModifiedDate
            };

            return Ok(new ResponseVM<ReportVM>
            {
                Code = StatusCodes.Status200OK,
                Status = StatusCodes.Status200OK.ToString(),
                Message = "Success",
                Data = reportVM
            });
        }
        catch (Exception)
        {
            throw;
        }
    }

    [HttpGet("GetReportByEmployeeId")]
    public IActionResult GetReportByEmployeeId(Guid employeeId)
    {
        try
        {
            var reports = _reportRepository.GetReportByEmployeeId(employeeId);
            if (!reports.Any())
            {
                return NotFound(new ResponseVM<ReportVM>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Not Found"
                });
            }

            var reportVM = reports.Select(r => new ReportVM
            {
                Guid = r.Guid,
                Subject = r.Subject,
                Description = r.Description,
                FileName = r.FileName,
                FileData = r.FileData,
                FileType = r.FileType,
                CreatedDate = r.CreatedDate,
                ModifiedDate = r.ModifiedDate
            });

            return Ok(new ResponseVM<IEnumerable<ReportVM>>
            {
                Code = StatusCodes.Status200OK,
                Status = StatusCodes.Status200OK.ToString(),
                Message = "Success",
                Data = reportVM
            });
        }
        catch (Exception)
        {
            throw;
        }
    }

}
