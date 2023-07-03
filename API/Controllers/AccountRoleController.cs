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

        [HttpGet("GetRoleManager")]
        public IActionResult GetRoleManager()
        {
            var query = _accountRoleRepository.GetRoleManager();
            if (query == null)
            {
                return BadRequest(new ResponseVM<RoleManagerVM>
                {
                    Code = StatusCodes.Status400BadRequest,
                    Status = HttpStatusCode.BadRequest.ToString(),
                    Message = "Get Role Manager failed",
                    Data = null
                });
            }
            return Ok(new ResponseVM<IEnumerable<RoleManagerVM>>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Get Role Manager successfully",
                Data = query
            });
        }

    }
}
