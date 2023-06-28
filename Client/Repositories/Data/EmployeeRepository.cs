using Client.Models;
using Client.Repositories.Interface;
using Client.ViewModels;
using Newtonsoft.Json;
using System.Text;

namespace Client.Repositories.Data
{
    public class EmployeeRepository : GeneralRepository<Employee, Guid>, IEmployeeRepository
    {
        private readonly HttpClient httpClient;
        private readonly string request;
        public EmployeeRepository(string request = "Employee/") : base(request)
        {
            httpClient = new HttpClient
            {
                BaseAddress = new Uri("https://localhost:7046/api/")
            };
            this.request = request;
        }

        public async Task<ResponseListVM<Employee>> GetEmp()
        {
            ResponseListVM<Employee> entityVM = null;
            using (var response = httpClient.GetAsync(request + "GetAllMasterEmployee").Result)
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                entityVM = JsonConvert.DeserializeObject<ResponseListVM<Employee>>(apiResponse);
            }
            return entityVM;
        }

        public async Task<ResponseListVM<EmployeeVM>> GetEmployeeByManagerID(Guid managerID)
        {
            ResponseListVM<EmployeeVM> entityVM = null;
            using (var response = await httpClient.GetAsync($"{request}GetEmployeeByManagerId?managerId={managerID}"))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();

                entityVM = JsonConvert.DeserializeObject<ResponseListVM<EmployeeVM>>(apiResponse);
                // ngecek kalo datanya cuma satu, diubah dulu ke response view model yg nerima satu data
                // abis itu diubah ke response list vm
                /* if (apiResponse.StartsWith("{"))
                 {
                     var employee = JsonConvert.DeserializeObject<ResponseViewModel<EmployeeVM>>(apiResponse);
                     entityVM = new ResponseListVM<EmployeeVM>
                     {
                         Data = new List<EmployeeVM> { employee.Data }
                     };
                 } else if (apiResponse.StartsWith("["))
                 {
                     entityVM = JsonConvert.DeserializeObject<ResponseListVM<EmployeeVM>>(apiResponse);

                 }*/
            }
            return entityVM;
        }
    }


}
