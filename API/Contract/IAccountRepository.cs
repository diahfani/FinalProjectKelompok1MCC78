using API.Model;
using API.ViewModel.Account;

namespace API.Contracts;

public interface IAccountRepository : IGenericRepository<Account>
{
    LoginVM Login (LoginVM loginVM);
    int Register (RegisterVM registerVM);
    IEnumerable<string> GetRoles(Guid guid);
}
