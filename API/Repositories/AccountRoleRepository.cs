using API.Contracts;
using API.Model;

namespace API.Repositories;

public class AccountRoleRepository : GeneralRepository<AccountRole>, IAccountRoleRepository
{
    public AccountRoleRepository(ProjectManagementDBContext dbContext) : base(dbContext)
    {
        
    }
}
