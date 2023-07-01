﻿using Client.Models;
using Client.Repositories.Interface;
using Client.ViewModels;
using Newtonsoft.Json;

namespace Client.Repositories.Data;

public class ReportRepository : GeneralRepository<Report, Guid>, IReportRepository
{
    private readonly HttpClient httpClient;
    private readonly string request;
    public ReportRepository(string request = "Report/") : base(request)
    {
        httpClient = new HttpClient
        {
            BaseAddress = new Uri("https://localhost:7046/api/")
        };
        this.request = request;
    }

    public async Task<string> DownloadReport(Guid ReportId)
    {
        /*ResponseViewModel<FileUploadAndDownlodVM> entityVM = null;*/
        var responseString = "";
        using (var response = httpClient.GetAsync($"{request}DownloadFile?guid={ReportId}").Result)
        {
            /*string apiResponse = await response.Content.ReadAsStringAsync();*/
            string apiResponse =  response.StatusCode.ToString();
            responseString = apiResponse;
            /*entityVM = JsonConvert.DeserializeObject<ResponseViewModel<FileUploadAndDownlodVM>>(apiResponse);*/
        }
        return responseString;
    }

    public async Task<ResponseListVM<Report>> GetReport()
    {
        ResponseListVM<Report> entityVM = null;
        using (var response = httpClient.GetAsync(request + "GetAllMasterEmployee").Result)
        {
            string apiResponse = await response.Content.ReadAsStringAsync();
            entityVM = JsonConvert.DeserializeObject<ResponseListVM<Report>>(apiResponse);
        }
        return entityVM;
    }

    public async Task<ResponseViewModel<ReportVM>> GetReportByTaskId(Guid TaskId)
    {
        ResponseViewModel<ReportVM> entityVM = null;
        using (var response = httpClient.GetAsync($"{request}GetReportByTaskId?taskId={TaskId}").Result)
        {
            string apiResponse = await response.Content.ReadAsStringAsync();
            entityVM = JsonConvert.DeserializeObject<ResponseViewModel<ReportVM>>(apiResponse);
        }
        return entityVM;
    }
}
