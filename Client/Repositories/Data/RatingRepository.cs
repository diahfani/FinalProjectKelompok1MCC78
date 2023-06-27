using Client.Models;
using Client.Repositories.Interface;
using Client.ViewModels;
using Newtonsoft.Json;

namespace Client.Repositories.Data;

public class RatingRepository : GeneralRepository<Rating, Guid>, IRatingRepository
{
    private readonly HttpClient httpClient;
    private readonly string request;
    public RatingRepository(string request = "Rating/") : base(request)
    {
        httpClient = new HttpClient
        {
            BaseAddress = new Uri("https://localhost:7046/api/")
        };
        this.request = request;
    }

    public async Task<ResponseListVM<Rating>> GetRating()
    {
        ResponseListVM<Rating> entityVM = null;
        using (var response = httpClient.GetAsync(request + "GetAllMasterEmployee").Result)
        {
            string apiResponse = await response.Content.ReadAsStringAsync();
            entityVM = JsonConvert.DeserializeObject<ResponseListVM<Rating>>(apiResponse);
        }
        return entityVM;
    }
}
