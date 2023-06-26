using Client.Models;
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
            BaseAddress = new Uri("https://localhost:7032/api/")
        };
        this.request = request;
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
}
