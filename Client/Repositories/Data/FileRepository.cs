using Client.Models;
using Client.Repositories.Interface;
using Client.ViewModels;
using Newtonsoft.Json;
using File = Client.Models.File;

namespace Client.Repositories.Data;

public class FileRepository : GeneralRepository<File, Guid>, IFileRepository
{
    private readonly HttpClient httpClient;
    private readonly string request;
    public FileRepository(string request = "File/") : base(request)
    {
        httpClient = new HttpClient
        {
            BaseAddress = new Uri("https://localhost:7032/api/")
        };
        this.request = request;
    }

    public async Task<ResponseListVM<File>> GetFile()
    {
        ResponseListVM<File> entityVM = null;
        using (var response = httpClient.GetAsync(request + "GetAllMasterEmployee").Result)
        {
            string apiResponse = await response.Content.ReadAsStringAsync();
            entityVM = JsonConvert.DeserializeObject<ResponseListVM<File>>(apiResponse);
        }
        return entityVM;
    }
}
