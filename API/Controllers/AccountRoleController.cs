using API.Contracts;
using API.Model;
using API.Repositories;
using API.ViewModel.AccountRole;
using API.ViewModel.Other;
using Microsoft.AspNetCore.Mvc;
using System.Drawing.Printing;
using System.Net;

namespace API.Controllers
{
    public class AccountRoleController : BaseController<AccountRole, AccountRoleVM>
    {
        private readonly IAccountRoleRepository _accountRoleRepository;
        private readonly IMapper<AccountRole,  AccountRoleVM> _mapper;

        public AccountRoleController(IAccountRoleRepository accountRoleRepository, IMapper<AccountRole, AccountRoleVM> mapper) : base(accountRoleRepository,mapper)
        {
            _accountRoleRepository = accountRoleRepository;
            _mapper = mapper;
        }
    }
}
