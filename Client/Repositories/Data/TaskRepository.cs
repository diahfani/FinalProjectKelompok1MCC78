using Client.Models;
using Client.Repositories.Interface;
using Client.ViewModels;
using Newtonsoft.Json;
using Task = Client.Models.Task;

namespace Client.Repositories.Data;

public class TaskRepository : GeneralRepository<Task, Guid>, ITaskRepository
{
    private readonly HttpClient httpClient;
    private readonly string request;
    public TaskRepository(string request = "Task/") : base(request)
    {
        httpClient = new HttpClient
        {
            BaseAddress = new Uri("https://localhost:7046/api/")
        };
        this.request = request;
    }

    public async Task<ResponseListVM<Task>> GetTask()
    {
        ResponseListVM<Task> entityVM = null;
        using (var response = httpClient.GetAsync(request + "GetAllMasterEmployee").Result)
        {
            string apiResponse = await response.Content.ReadAsStringAsync();
            entityVM = JsonConvert.DeserializeObject<ResponseListVM<Task>>(apiResponse);
        }
        return entityVM;
    }
}
