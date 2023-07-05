using API.Contracts;
using API.Model;
using API.Utilities;
using API.ViewModel.Account;
using System.Linq.Expressions;

namespace API.Repositories;

public class AccountRepository : GeneralRepository<Account>, IAccountRepository
{
    private readonly IEmployeeRepository _employeeRepository;

    public AccountRepository(ProjectManagementDBContext context, IEmployeeRepository employeeRepository) : base(context)
    {
        _employeeRepository = employeeRepository;
    }

    public IEnumerable<string> GetRoles(Guid guid)
    {
        var account = GetByGuid(guid);
        if (account is null) return Enumerable.Empty<string>();
        var query = from accRole in _context.AccountRoles
                    join role in _context.Roles
                    on accRole.RoleGuid equals role.Guid
                    where accRole.AccountGuid == guid
                    select role.Name;
        return query.ToList();
    }

    public LoginVM Login(LoginVM loginVM)
    {
        var account = GetAll();
        var employee = _context.Employees.ToList();
        var query = from emp in employee
                    join acc in account
                    on emp.Guid equals acc.Guid
                    where emp.Email == loginVM.Email
                    select new LoginVM
                    {
                        Email = emp.Email,
                        Password = acc.Password
                    };

        return query.FirstOrDefault();

    }

    public int Register(RegisterVM registerVM)
    {
        /*throw new NotImplementedException();*/

        try
        {
            if (registerVM.ManagerID == Guid.Empty || registerVM.ManagerID == null)
            {
                registerVM.ManagerID = null;
            }
            var employee = new Employee
            {
                NIK = registerVM.NIK,
                Fullname = registerVM.Fullname,
                Gender = registerVM.Gender,
                Email = registerVM.Email,
                PhoneNumber = registerVM.PhoneNumber,
                HiringDate = registerVM.HiringDate,
                ManagerID = registerVM.ManagerID,
            };
            var result = _employeeRepository.Create(employee);

            var hashingPassword = Hashing.HashPassword(registerVM.Password);
            var account = new Account
            {
                Guid = employee.Guid,
                Password = hashingPassword,
            };
            Create(account);

            var accountRole = new AccountRole
            {
                RoleGuid = Guid.Parse("BAD2010A-8D51-4EAF-ECCB-08DB73D114FF"),
                AccountGuid = employee.Guid,
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now
            };
            _context.Set<AccountRole>().Add(accountRole);
            _context.SaveChanges();
            return 3;
        }
        catch
        {
            return 0;
        }

    }
}
