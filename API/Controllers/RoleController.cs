using API.Contracts;
using API.Model;
using API.ViewModel.Role;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RoleController : BaseController<Role, RoleVM>
{
    private readonly IRoleRepository _roleRepository;
    public RoleController(IRoleRepository roleRepository, IMapper<Role, RoleVM> mapper)
        : base(roleRepository, mapper)
    {
        _roleRepository = roleRepository;
    }

}
