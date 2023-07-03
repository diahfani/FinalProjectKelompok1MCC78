using Client.Repositories.Interface;
using Microsoft.AspNetCore.Mvc;

namespace Client.Controllers;

public class AccountRoleController : Controller
{
    private readonly IAccountRoleRepository repository;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public AccountRoleController(IAccountRoleRepository repository, IHttpContextAccessor httpContextAccessor)
    {
        this.repository = repository;
        _httpContextAccessor = httpContextAccessor;
    }



}
