using Client.Models;
using Client.Repositories.Interface;
using Client.ViewModels;
using Newtonsoft.Json;
using System.Text;

namespace Client.Repositories.Data;

public class AccountRoleRepository : GeneralRepository<AccountRole, string>, IAccountRoleRepository
{
    private readonly HttpClient httpClient;
    private readonly string request;

    public AccountRoleRepository(string request = "AccountRole/") : base(request)
    {
        httpClient = new HttpClient
        {
            BaseAddress = new Uri("https://localhost:7046/api/")
        };
        this.request = request;
    }

    public Task<ResponseMessageVM> Deletes(Guid guid)
    {
        throw new NotImplementedException();
    }

    public Task<ResponseViewModel<AccountRole>> Get(Guid guid)
    {
        throw new NotImplementedException();
    }

    public async Task<ResponseListVM<RoleManagerVM>> GetRoleManager()
    {
        ResponseListVM<RoleManagerVM> responseListVM = null;
        StringContent content = new StringContent(JsonConvert.SerializeObject(responseListVM), Encoding.UTF8, "application/json");
        using (var response = httpClient.GetAsync(request + "GetRoleManager").Result)
        {
            string apiResponse = await response.Content.ReadAsStringAsync();
            responseListVM = JsonConvert.DeserializeObject<ResponseListVM<RoleManagerVM>>(apiResponse);
        }
        return responseListVM;

    }
}
