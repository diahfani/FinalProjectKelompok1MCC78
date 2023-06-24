using API.Contracts;
using API.Model;

namespace API.Repositories;

public class RoleRepository : GeneralRepository<Role>, IRoleRepository
{
    public RoleRepository(ProjectManagementDBContext context) : base(context)
    {
        
    }
}
