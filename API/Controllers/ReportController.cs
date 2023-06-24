﻿using API.Contracts;
using API.Model;
using API.ViewModel.Other;
using API.ViewModel.Report;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace API.Controllers;

public class ReportController : BaseController<Report, ReportVM>
{
    private readonly IReportRepository _reportRepository;
    

    public ReportController(IReportRepository reportRepository, IMapper<Report, ReportVM> mapper) : base(reportRepository, mapper)
    {
        _reportRepository = reportRepository;
    }

    [HttpPost("PostSingleFile")]
    public async Task<IActionResult> PostSingleFile([FromForm] ReportVM reportvm)
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

    [HttpGet("DownloadFile")]
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
}
