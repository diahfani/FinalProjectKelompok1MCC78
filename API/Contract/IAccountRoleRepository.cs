using API.Model;
using API.ViewModel.AccountRole;

namespace API.Contracts
{
    public interface IAccountRoleRepository : IGenericRepository<AccountRole>
    {
        public IEnumerable<RoleManagerVM> GetRoleManager();
    }
}
