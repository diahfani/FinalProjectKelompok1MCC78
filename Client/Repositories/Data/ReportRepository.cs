using Client.Models;
using Client.Repositories.Interface;
using Client.ViewModels;
using Newtonsoft.Json;
using System.Text;

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
            string apiResponse = response.StatusCode.ToString();
            responseString = apiResponse;
            /*entityVM = JsonConvert.DeserializeObject<ResponseViewModel<FileUploadAndDownlodVM>>(apiResponse);*/
        }
        return responseString;
    }

    public async Task<ResponseListVM<ReportVM>> GetReport()
    {
        ResponseListVM<ReportVM> entityVM = null;
        using (var response = httpClient.GetAsync(request + "GetReport").Result)
        {
            string apiResponse = await response.Content.ReadAsStringAsync();
            entityVM = JsonConvert.DeserializeObject<ResponseListVM<ReportVM>>(apiResponse);
        }
        return entityVM;
    }

    public async Task<ResponseViewModel<Models.Report>> GetReportByTaskId(Guid TaskId)
    {
        ResponseViewModel<Models.Report> entityVM = null;
        using (var response = httpClient.GetAsync($"{request}GetReportByTaskId?taskId={TaskId}").Result)
        {
            string apiResponse = await response.Content.ReadAsStringAsync();
            entityVM = JsonConvert.DeserializeObject<ResponseViewModel<Models.Report>>(apiResponse);
        }
        return entityVM;
    }

    public async Task<ResponseViewModel<Models.File>> Post(Models.File file)
    {
        ResponseViewModel<Models.File> entityVM = null;
        StringContent content = new StringContent(JsonConvert.SerializeObject(file), Encoding.UTF8, "application/json");
        using (var response = httpClient.PostAsync(request + "PostSingleFile", content).Result)
        {
            string apiResponse = await response.Content.ReadAsStringAsync();
            entityVM = JsonConvert.DeserializeObject<ResponseViewModel<Models.File>>(apiResponse);
        }
        return entityVM;
    }

    public async Task<ResponseViewModel<Models.File>> Put(Models.File file)
    {
        ResponseViewModel<Models.File> entityVM = null;
        StringContent content = new StringContent(JsonConvert.SerializeObject(file), Encoding.UTF8, "application/son");
        using (var response = httpClient.PutAsync(request + "EditReport", content).Result)
        {
            string apiResponse = await response.Content.ReadAsStringAsync();
            entityVM = JsonConvert.DeserializeObject<ResponseViewModel<Models.File>>(apiResponse);
        }
        return entityVM;
    }
}
