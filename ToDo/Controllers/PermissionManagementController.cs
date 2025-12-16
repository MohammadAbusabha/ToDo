using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using ToDo.Interfaces;
using ToDo.Resources;

namespace ToDo.Controllers
{
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin")]
    [ApiController]
    public class PermissionManagementController : ControllerBase
    {
        private readonly IPrivilegeManagementService _permissionManagementService;
        public PermissionManagementController(IPrivilegeManagementService permissionManagementService)
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
