using Client.Models;
using Client.ViewModels;

namespace Client.Repositories.Interface;

public interface IAccountRoleRepository : IRepository<AccountRole, Guid>
{
    public Task<ResponseListVM<RoleManagerVM>> GetRoleManager();

}
