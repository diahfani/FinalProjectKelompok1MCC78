using API.Contracts;
using API.Model;
using API.ViewModel.AccountRole;

namespace API.Repositories;

public class AccountRoleRepository : GeneralRepository<AccountRole>, IAccountRoleRepository
{
    private readonly IAccountRepository _accountRepository;
    private readonly IRoleRepository _roleRepository;
    private readonly IEmployeeRepository _employeeRepository;
    public AccountRoleRepository(ProjectManagementDBContext dbContext, IAccountRepository accountRepository, IRoleRepository roleRepository, 
        IEmployeeRepository employeeRepository) : base(dbContext)
    {
        _accountRepository = accountRepository;
        _roleRepository = roleRepository;
        _employeeRepository = employeeRepository;
    }

    public IEnumerable<RoleManagerVM> GetRoleManager()
    {
        var getAllAccountRole = GetAll();
        var getAllRole = _roleRepository.GetAll();
        var getAllEmployee = _employeeRepository.GetAll();
        var getAllAccount = _accountRepository.GetAll();
        var query = from accRole in getAllAccountRole
                    join role in getAllRole
                    on accRole.RoleGuid equals role.Guid
                    join acc in getAllAccount
                    on accRole.AccountGuid equals acc.Guid
                    join emp in getAllEmployee
                    on accRole.AccountGuid equals emp.Guid
                    where accRole.RoleGuid == Guid.Parse("F0ED952A-0321-4193-3653-08DB73D30B74")
                    select new RoleManagerVM
                    {
                        Guid = accRole.Guid,
                        AccountGuid = accRole.AccountGuid,
                        ManagerName = emp.Fullname,
                        RoleGuid = accRole.RoleGuid,
                        RoleName = role.Name,
                    };
        return query.ToList();
    }
}
