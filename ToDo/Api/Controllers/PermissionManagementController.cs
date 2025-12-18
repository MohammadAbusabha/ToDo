using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using ToDo.Infrastructure.Interfaces;
using ToDo.Infrastructure.Resources;

namespace ToDo.Api.Controllers
{
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin")]
    [ApiController]
    public class PermissionManagementController : ControllerBase
    {
        private readonly IPrivilegeManagementService _permissionManagementService;
        public PermissionManagementController(IPrivilegeManagementService permissionManagementService)// Controller useless for now
        {
            _permissionManagementService = permissionManagementService;
        }

        [HttpPost]
        public async Task AddPermissions(PrivilegeResource privilegeResource)
        {
            //await _permissionManagementService.Addpermission(permissionResource);
        }
    }
}
